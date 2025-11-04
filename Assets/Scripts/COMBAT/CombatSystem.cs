using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

namespace Game.Combat
{
    public static class CombatSystem
    {
        // Compatibility overloads
        public static void Attack(
            CharacterAttributes attackerAttr,
            CombatStats attackerStats,
            CharacterAttributes defenderAttr,
            CombatStats defenderStats,
            MeleeWeapon weapon,
            bool isCrit = false)
        {
            Attack(attackerAttr, attackerStats, defenderAttr, defenderStats, weapon, null, isCrit);
        }

        public static void RangedAttack(
            CharacterAttributes attackerAttr,
            CombatStats attackerStats,
            CharacterAttributes defenderAttr,
            CombatStats defenderStats,
            RangedWeapon weapon,
            Projectile projectile,
            bool isCrit = false,
            bool isHeadshot = false)
        {
            RangedAttack(attackerAttr, attackerStats, defenderAttr, defenderStats, weapon, projectile, null, isCrit, isHeadshot);
        }

        // Core implementations
        public static void Attack(
            CharacterAttributes attackerAttr,
            CombatStats attackerStats,
            CharacterAttributes defenderAttr,
            CombatStats defenderStats,
            MeleeWeapon weapon,
            GameObject defenderObj = null,
            bool isCrit = false)
        {
            if (attackerStats == null || defenderStats == null) { Debug.LogWarning("Attack: stats null"); return; }
            if (weapon == null) { Debug.LogWarning("Attack: weapon null"); return; }

            float damage = isCrit ? weapon.CritDamage : weapon.BaseDamage;
            damage *= GetDefenseModifier(defenderStats.Defense);

            defenderStats.Health -= damage;
            Debug.Log($"{ToNameSafe(attackerAttr?.Race)} hits {ToNameSafe(defenderAttr?.Race)} for {damage} damage");

            if (defenderObj != null)
            {
                var status = defenderObj.GetComponent<StatusEffectHandler>();
                if (status != null)
                {
                    if (Random.value < attackerStats.BleedChance) Debug.Log("Bleed triggered");
                }
            }
        }

        public static void RangedAttack(
            CharacterAttributes attackerAttr,
            CombatStats attackerStats,
            CharacterAttributes defenderAttr,
            CombatStats defenderStats,
            RangedWeapon weapon,
            Projectile projectile,
            GameObject defenderObj,
            bool isCrit = false,
            bool isHeadshot = false)
        {
            if (attackerStats == null || defenderStats == null) { Debug.LogWarning("RangedAttack: stats null"); return; }
            if (weapon == null || projectile == null) { Debug.LogWarning("RangedAttack: null projectile/weapon"); return; }

            float damage = isCrit ? projectile.CritDamage : projectile.BaseDamage;
            if (isHeadshot)
            {
                if (projectile.HeadshotFatal) { defenderStats.Health = 0; return; }
                if (projectile.HeadshotExtraDamage > 0) damage += projectile.HeadshotExtraDamage;
            }

            damage *= GetDefenseModifier(defenderStats.Defense);
            defenderStats.Health -= damage;
        }

        private static float GetDefenseModifier(float defense)
        {
            float modifier = 1f - (defense / 100f);
            return Mathf.Clamp(modifier, 0.2f, 1f);
        }

        private static string ToNameSafe(object raceObj)
        {
            if (raceObj == null) return "Unknown";
            var e = raceObj as System.Enum;
            if (e != null) return e.ToString();
            try { return raceObj.ToString(); } catch { return "Unknown"; }
        }

        // Convert a global (no-namespace) CombatStats into a namespaced Game.Combat.CombatStats
        private static CombatStats ConvertFromGlobalCombatStats(global::CombatStats g)
        {
            if (g == null) return null;
            return new CombatStats
            {
                Health = g.Health,
                Stamina = g.Stamina,
                AttackPower = g.AttackPower,
                Defense = g.Defense,
                BleedChance = g.BleedChance,
                DismemberChance = g.DismemberChance
            };
        }

        // Convert namespaced -> global if needed
        private static global::CombatStats ConvertToGlobalCombatStats(CombatStats ns)
        {
            if (ns == null) return null;
            var g = new global::CombatStats();
            g.Health = ns.Health;
            g.Stamina = ns.Stamina;
            g.AttackPower = ns.AttackPower;
            g.Defense = ns.Defense;
            g.BleedChance = ns.BleedChance;
            g.DismemberChance = ns.DismemberChance;
            return g;
        }

        /// <summary>
        /// Restituisce sempre un Game.Combat.CombatStats (namespaced).
        /// Converte esplicitamente qualsiasi componente global::CombatStats prima di restituire.
        /// Reflection fallback: verifica il valore restituito (val is ...) e converte dove necessario.
        /// </summary>
        private static CombatStats GetCombatStatsFromObject(GameObject obj)
        {
            if (obj == null) return null;

            // 1) StatusEffectHandler (preferred)
            var status = obj.GetComponent<StatusEffectHandler>();
            if (status != null && status.combatStats != null)
            {
                // status.combatStats potrebbe essere:
                // - Game.Combat.CombatStats (namespaced) -> restituiscilo direttamente
                // - global::CombatStats (legacy) -> converti con ConvertFromGlobalCombatStats
                object sObj = status.combatStats;

                if (sObj is Game.Combat.CombatStats nsVal) return nsVal;
                if (sObj is global::CombatStats gVal) return ConvertFromGlobalCombatStats(gVal);

                // fallback: prova con cast 'as' (nel caso sia boxed o diverso contesto)
                try
                {
                    var maybeGlobal = sObj as global::CombatStats;
                    if (maybeGlobal != null) return ConvertFromGlobalCombatStats(maybeGlobal);
                }
                catch { /* ignore */ }
            }

            // 2) Directly attached namespaced CombatStats component (generic GetComponent<T>())
            try
            {
                var nsComp = obj.GetComponent<CombatStats>();
                if (nsComp != null) return nsComp;
            }
            catch { /* type might not be present on some projects */ }

            // 3) Directly attached global CombatStats component (generic GetComponent<T>()), convert it
            try
            {
                var globalComp = obj.GetComponent<global::CombatStats>();
                if (globalComp != null) return ConvertFromGlobalCombatStats(globalComp);
            }
            catch { /* global type might not be present */ }

            // 4) Reflection fallback: inspect properties/fields on attached MonoBehaviours
            var comps = obj.GetComponents<MonoBehaviour>();
            foreach (var c in comps)
            {
                if (c == null) continue;
                var t = c.GetType();

                // Properties
                foreach (var prop in t.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    object raw = null;
                    try { raw = prop.GetValue(c); } catch { raw = null; }

                    if (raw == null) continue;

                    if (raw is CombatStats nsVal) return nsVal;
                    if (raw is global::CombatStats gVal) return ConvertFromGlobalCombatStats(gVal);
                }

                // Fields
                foreach (var field in t.GetFields(BindingFlags.Public | BindingFlags.Instance))
                {
                    object raw = null;
                    try { raw = field.GetValue(c); } catch { raw = null; }

                    if (raw == null) continue;

                    if (raw is CombatStats nsVal) return nsVal;
                    if (raw is global::CombatStats gVal) return ConvertFromGlobalCombatStats(gVal);
                }
            }

            // Nothing found
            return null;
        }
    }
}
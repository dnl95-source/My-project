// Wrapper globale per compatibilità: inoltra le chiamate a Game.Combat.CombatSystem
// Converte tra la definizione globale 'CombatStats' e 'Game.Combat.CombatStats'.
using UnityEngine;

public static class CombatSystem
{
    // Convert global -> namespaced
    private static Game.Combat.CombatStats ToNamespaced(CombatStats s)
    {
        if (s == null) return null;
        return new Game.Combat.CombatStats
        {
            Health = s.Health,
            Stamina = s.Stamina,
            AttackPower = s.AttackPower,
            Defense = s.Defense,
            BleedChance = s.BleedChance,
            DismemberChance = s.DismemberChance
        };
    }

    // Copy values back from namespaced -> global
    private static void CopyBack(Game.Combat.CombatStats src, CombatStats dst)
    {
        if (src == null || dst == null) return;
        dst.Health = src.Health;
        dst.Stamina = src.Stamina;
        dst.AttackPower = src.AttackPower;
        dst.Defense = src.Defense;
        dst.BleedChance = src.BleedChance;
        dst.DismemberChance = src.DismemberChance;
    }

    public static void Attack(
        CharacterAttributes attackerAttr,
        CombatStats attackerStats,
        CharacterAttributes defenderAttr,
        CombatStats defenderStats,
        MeleeWeapon weapon,
        bool isCrit = false)
    {
        var aNs = ToNamespaced(attackerStats);
        var dNs = ToNamespaced(defenderStats);

        // FORCED fully-qualified call to the namespaced combat system
        Game.Combat.CombatSystem.Attack(attackerAttr, aNs, defenderAttr, dNs, weapon, null, isCrit);

        // copy back any mutated values
        CopyBack(aNs, attackerStats);
        CopyBack(dNs, defenderStats);
    }

    public static void Attack(
        CharacterAttributes attackerAttr,
        CombatStats attackerStats,
        CharacterAttributes defenderAttr,
        CombatStats defenderStats,
        MeleeWeapon weapon,
        GameObject defenderObj,
        bool isCrit = false)
    {
        var aNs = ToNamespaced(attackerStats);
        var dNs = ToNamespaced(defenderStats);

        Game.Combat.CombatSystem.Attack(attackerAttr, aNs, defenderAttr, dNs, weapon, defenderObj, isCrit);

        CopyBack(aNs, attackerStats);
        CopyBack(dNs, defenderStats);
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
        var aNs = ToNamespaced(attackerStats);
        var dNs = ToNamespaced(defenderStats);

        Game.Combat.CombatSystem.RangedAttack(attackerAttr, aNs, defenderAttr, dNs, weapon, projectile, null, isCrit, isHeadshot);

        CopyBack(aNs, attackerStats);
        CopyBack(dNs, defenderStats);
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
        var aNs = ToNamespaced(attackerStats);
        var dNs = ToNamespaced(defenderStats);

        Game.Combat.CombatSystem.RangedAttack(attackerAttr, aNs, defenderAttr, dNs, weapon, projectile, defenderObj, isCrit, isHeadshot);

        CopyBack(aNs, attackerStats);
        CopyBack(dNs, defenderStats);
    }
}
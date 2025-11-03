using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace Game.Characters
{
    using RaceType = Game.Combat.RaceType; // alias per chiarezza

    public static class CharacterGenerator
    {
        public static CharacterAttributes GenerateCharacter()
        {
            return GenerateCharacter("Player", RaceType.Human, 1);
        }

        public static CharacterAttributes GenerateCharacter(string name)
        {
            return GenerateCharacter(name, RaceType.Human, 1);
        }

        public static CharacterAttributes GenerateCharacter(string name, RaceType race)
        {
            return GenerateCharacter(name, race, 1);
        }

        public static CharacterAttributes GenerateCharacter(string name, RaceType race, int level)
        {
            var attr = new CharacterAttributes();

            SetFieldOrPropertyIfExists(attr, "Race", race);
            SetFieldOrPropertyIfExists(attr, "race", race);
            SetFieldOrPropertyIfExists(attr, "Name", name);
            SetFieldOrPropertyIfExists(attr, "name", name);
            SetFieldOrPropertyIfExists(attr, "Level", level);
            SetFieldOrPropertyIfExists(attr, "level", level);

            SetFieldOrPropertyIfExists(attr, "Health", GetDefaultHealthForRace(race));
            SetFieldOrPropertyIfExists(attr, "Stamina", 100f);
            SetFieldOrPropertyIfExists(attr, "AttackPower", 10f + level);
            SetFieldOrPropertyIfExists(attr, "Defense", 5f);

            return attr;
        }

        public static CharacterAttributes GenerateCharacter(params object[] args)
        {
            if (args == null || args.Length == 0)
                return GenerateCharacter();

            foreach (var a in args)
            {
                if (a is CharacterAttributes ca) return ca;
            }

            string name = null;
            RaceType race = RaceType.Human;
            int level = 1;
            float? explicitHealth = null;
            var additionalProps = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            foreach (var a in args)
            {
                if (a == null) continue;

                if (a is string s && name == null) { name = s; continue; }
                if (a is RaceType rt) { race = rt; continue; }
                if (a is int i && level == 1) { level = i; continue; }
                if (a is float f && explicitHealth == null) { explicitHealth = f; continue; }

                if (a is IDictionary dict)
                {
                    foreach (DictionaryEntry entry in dict)
                    {
                        var key = entry.Key?.ToString();
                        if (string.IsNullOrEmpty(key)) continue;
                        additionalProps[key] = entry.Value;
                    }
                    continue;
                }

                var type = a.GetType();
                var propName = type.GetProperty("Name", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)
                           ?? type.GetProperty("name", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                var propRace = type.GetProperty("Race", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)
                           ?? type.GetProperty("race", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                var propLevel = type.GetProperty("Level", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)
                           ?? type.GetProperty("level", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                var propHealth = type.GetProperty("Health", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)
                           ?? type.GetProperty("health", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                try
                {
                    if (propName != null && name == null)
                    {
                        var val = propName.GetValue(a);
                        if (val is string ns) name = ns;
                    }
                }
                catch { }

                try
                {
                    if (propRace != null)
                    {
                        var val = propRace.GetValue(a);
                        if (val is RaceType rtv) race = rtv;
                        else { try { race = (RaceType)Enum.Parse(typeof(RaceType), val?.ToString() ?? "", true); } catch { } }
                    }
                }
                catch { }

                try
                {
                    if (propLevel != null)
                    {
                        var val = propLevel.GetValue(a);
                        if (val is int iv) level = iv;
                        else { if (int.TryParse(val?.ToString(), out int parsed)) level = parsed; }
                    }
                }
                catch { }

                try
                {
                    if (propHealth != null && explicitHealth == null)
                    {
                        var val = propHealth.GetValue(a);
                        if (val is float hf) explicitHealth = hf;
                        else if (float.TryParse(val?.ToString(), out float parsedF)) explicitHealth = parsedF;
                    }
                }
                catch { }
            }

            if (additionalProps.Count > 0)
            {
                if (name == null)
                {
                    if (additionalProps.TryGetValue("Name", out var nVal) || additionalProps.TryGetValue("name", out nVal))
                        name = nVal?.ToString();
                }

                if (additionalProps.TryGetValue("Race", out var rVal) || additionalProps.TryGetValue("race", out rVal))
                {
                    if (rVal is RaceType rtv) race = rtv;
                    else if (Enum.TryParse(typeof(RaceType), rVal?.ToString() ?? "", true, out var parsedRace))
                        race = (RaceType)parsedRace;
                }

                if (additionalProps.TryGetValue("Level", out var lVal) || additionalProps.TryGetValue("level", out lVal))
                {
                    if (lVal is int lv) level = lv;
                    else if (int.TryParse(lVal?.ToString(), out int parsed)) level = parsed;
                }

                if (additionalProps.TryGetValue("Health", out var hVal) || additionalProps.TryGetValue("health", out hVal))
                {
                    if (hVal is float hf) explicitHealth = hf;
                    else if (float.TryParse(hVal?.ToString(), out float parsedF)) explicitHealth = parsedF;
                }
            }

            var result = GenerateCharacter(name ?? "Player", race, level);

            if (explicitHealth != null) SetFieldOrPropertyIfExists(result, "Health", explicitHealth.Value);

            foreach (var kvp in additionalProps)
            {
                try { if (kvp.Value == null) continue; SetFieldOrPropertyIfExists(result, kvp.Key, kvp.Value); } catch { }
            }

            return result;
        }

        private static void SetFieldOrPropertyIfExists(object target, string memberName, object value)
        {
            if (target == null || string.IsNullOrEmpty(memberName)) return;

            var type = target.GetType();

            var prop = type.GetProperty(memberName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (prop != null && prop.CanWrite)
            {
                try
                {
                    var tVal = ConvertIfNeeded(value, prop.PropertyType);
                    if (tVal != null) prop.SetValue(target, tVal);
                    return;
                }
                catch { return; }
            }

            var field = type.GetField(memberName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (field != null)
            {
                try
                {
                    var tVal = ConvertIfNeeded(value, field.FieldType);
                    if (tVal != null) field.SetValue(target, tVal);
                    return;
                }
                catch { return; }
            }
        }

        private static object ConvertIfNeeded(object value, System.Type targetType)
        {
            if (value == null) return null;
            var valType = value.GetType();
            if (targetType.IsAssignableFrom(valType)) return value;

            try
            {
                if (targetType.IsEnum)
                {
                    if (value is string s)
                    {
                        if (Enum.TryParse(targetType, s, true, out var enumVal)) return enumVal;
                    }
                    else { return Enum.ToObject(targetType, value); }
                }

                return Convert.ChangeType(value, targetType);
            }
            catch { return null; }
        }

        private static float GetDefaultHealthForRace(RaceType race)
        {
            switch (race)
            {
                case RaceType.Dwarf: return 150f;
                case RaceType.Orc: return 140f;
                case RaceType.Goblin: return 80f;
                case RaceType.Elf: return 100f;
                case RaceType.ElfNature: return 110f;
                case RaceType.ElfWhite: return 105f;
                case RaceType.Human:
                default: return 120f;
            }
        }
    }
}
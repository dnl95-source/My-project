using UnityEngine;
using Game.Combat;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Singleton minimale NPCGenerator che espone Instance.GenerateNPC(...)
/// Ora accetta params object[] per coprire diverse chiamate con ordine/parametri variabili.
/// </summary>
public class NPCGenerator : MonoBehaviour
{
    private static NPCGenerator _instance;
    public static NPCGenerator Instance
    {
        get
        {
            if (_instance == null)
            {
                var go = GameObject.Find("NPCGenerator");
                if (go == null)
                {
                    go = new GameObject("NPCGenerator");
                    _instance = go.AddComponent<NPCGenerator>();
                    DontDestroyOnLoad(go);
                }
                else
                {
                    _instance = go.GetComponent<NPCGenerator>() ?? go.AddComponent<NPCGenerator>();
                }
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null) { _instance = this; DontDestroyOnLoad(gameObject); }
        else if (_instance != this) Destroy(gameObject);
    }

    // Backwards-compatible signature (original)
    public NPC GenerateNPC(string name = "NPC", RaceType race = RaceType.Human, int level = 1)
    {
        var npc = new NPC();
        npc.NPCName = name;
        npc.attributes = new CharacterAttributes();

        // try set Race if present
        var attr = npc.attributes;
        if (attr != null)
        {
            var attrType = attr.GetType();
            var prop = attrType.GetProperty("Race");
            if (prop != null && prop.PropertyType.IsAssignableFrom(typeof(RaceType)))
                prop.SetValue(attr, race);
            else
            {
                var field = attrType.GetField("Race");
                if (field != null && field.FieldType.IsAssignableFrom(typeof(RaceType)))
                    field.SetValue(attr, race);
            }
        }

        return npc;
    }

    // Flexible overload: accepts any combination/order of params and interprets them
    public NPC GenerateNPC(params object[] args)
    {
        string name = "NPC";
        RaceType race = RaceType.Human;
        int level = 1;

        if (args != null && args.Length > 0)
        {
            foreach (var a in args)
            {
                if (a == null) continue;
                if (a is string s && name == "NPC")
                {
                    name = s;
                    continue;
                }
                if (a is RaceType rt)
                {
                    race = rt;
                    continue;
                }
                if (a is int i)
                {
                    level = i;
                    continue;
                }
                // If other types are passed (Sex, Kingdom), you can extract them here if NPC has fields to set.
                // For now we ignore unknown types but you can extend.
            }
        }

        return GenerateNPC(name, race, level);
    }
}
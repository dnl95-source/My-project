using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "FactionData", menuName = "Game/Data/Faction")]
public class FactionData : ScriptableObject
{
    [Header("Identità")]
    public string factionId; // es. "ghestard"
    public string displayName; // es. "Ghestard"
    [TextArea(3,6)] public string description;

    [Header("Geografia e cultura")]
    public string capitalCity;
    public BiomeType[] primaryBiomes; // enum BiomeType definito in BiomeType.cs
    public TechLevel techLevel; // enum Low, Medium, High

    [Header("Allineamento e relazioni")]
    public Alignment alignment; // enum Good, Neutral, Evil
    public List<string> allies; // factionId list
    public List<string> enemies; // factionId list
    public List<string> neutrals;

    [Header("Heraldry & Visual")]
    public Color primaryColor = Color.white;
    public Color secondaryColor = Color.gray;
    public Texture2D bannerIcon; // opzionale

    [Header("Gameplay stats")]
    public int militaryStrength; // valore relativo per bilanciamento
    public int wealthIndex; // determina mercato, prezzo item
    public float spawnPriority; // quanto spesso appaiono NPC di questa fazione

    [Header("Key NPCs")]
    public List<FactionNPC> keyNPCs; // array con Re, GranCavaliere, Principessa ecc.
}

[System.Serializable]
public struct FactionNPC
{
    public string role;
    public string npcName;
    public string shortDescription;
}

public enum TechLevel { Low, Medium, High }
public enum Alignment { Good, Neutral, Evil }
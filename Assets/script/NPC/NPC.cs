using UnityEngine;

// Classe minimale per rappresentare i dati di un NPC.
// Adattala aggiungendo campi (inventory, behaviour, id, ecc.) se ti servono.
[System.Serializable]
public class NPC
{
    public string NPCName;
    public CharacterAttributes attributes;
    public int Level = 1;

    public NPC() { }

    public NPC(string name, CharacterAttributes attrs, int level = 1)
    {
        NPCName = name;
        attributes = attrs;
        Level = level;
    }
}
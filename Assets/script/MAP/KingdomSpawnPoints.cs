using UnityEngine;

public static class KingdomSpawnPoints
{
    public static Vector3 GetSpawnPoint(Kingdom kingdom)
    {
        switch (kingdom)
        {
            case Kingdom.NatureElves: return new Vector3(0, 0, 0);
            case Kingdom.HumanKingdom1: return new Vector3(50, 0, 0);
            case Kingdom.HumanKingdom2: return new Vector3(-50, 0, 0);
            case Kingdom.Dwarves: return new Vector3(0, 0, 50);
            case Kingdom.WhiteElves: return new Vector3(0, 0, -50);
            case Kingdom.DarkElves: return new Vector3(100, 0, 0);
            case Kingdom.Orcs: return new Vector3(-100, 0, 0);
            case Kingdom.Goblins: return new Vector3(0, 0, 100);
            case Kingdom.Ogres: return new Vector3(0, 0, -100);
            case Kingdom.Godrik: return new Vector3(150, 0, 0);
            default: return Vector3.zero;
        }
    }
}
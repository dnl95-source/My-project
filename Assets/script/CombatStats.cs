using System;

 // Classe CombatStats nel namespace globale per compatibilità con script legacy.
 // Questa definizione è intenzionalmente equivalente a Game.Combat.CombatStats.
 // In futuro puoi consolidare tutto in un unico namespace e rimuovere il duplicato.
[Serializable]
public class CombatStats
{
    public float Health = 100f;
    public float Stamina = 100f;
    public float AttackPower = 10f;
    public float Defense = 5f;
    public float BleedChance = 0.0f;
    public float DismemberChance = 0.0f;

    // Utility
    public bool IsAlive => Health > 0f;
}
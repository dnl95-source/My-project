using System;

namespace Game.Combat
{
    // Classe minimale CombatStats nel namespace Game.Combat.
    // Aggiungi o modifica i campi/proprietà in base alla tua implementazione reale.
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
}
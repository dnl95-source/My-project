using UnityEngine;
using Game.Combat; // opzionale: solo se usi Game.Combat nel codice economy

[RequireComponent(typeof(StatusEffectHandler))]
public class PlayerEconomy : MonoBehaviour
{
    public int Gold = 0;
    public int Silver = 0;

    // eventuali riferimenti a componenti del player
    private StatusEffectHandler statusHandler;

    void Awake()
    {
        statusHandler = GetComponent<StatusEffectHandler>();
    }

    public void AddGold(int amount)
    {
        if (amount <= 0) return;
        Gold += amount;
        Debug.Log($"PlayerEconomy: aggiunti {amount} gold. Totale: {Gold}");
    }

    public bool TrySpendGold(int amount)
    {
        if (amount <= 0) return true;
        if (Gold >= amount)
        {
            Gold -= amount;
            Debug.Log($"PlayerEconomy: spesi {amount} gold. Rimangono: {Gold}");
            return true;
        }
        Debug.LogWarning($"PlayerEconomy: fondi insufficienti per spendere {amount}. Totale: {Gold}");
        return false;
    }

    // Esempio di integrazione con il combat system (se necessario)
    public void RewardForKill(int rewardGold)
    {
        AddGold(rewardGold);
    }

    // Debug / editor helper
    [ContextMenu("Give 100 Gold")]
    private void DebugGiveGold()
    {
        AddGold(100);
    }
}
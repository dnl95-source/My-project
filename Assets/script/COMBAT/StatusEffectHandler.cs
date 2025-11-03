using UnityEngine;
using System.Collections;

public class StatusEffectHandler : MonoBehaviour
{
    public CombatStats combatStats;

    private Coroutine poisonCoroutine;
    private Coroutine fireCoroutine;

    public void StartPoison(float dps, float duration, float maxTotal)
    {
        if (poisonCoroutine != null) StopCoroutine(poisonCoroutine);
        poisonCoroutine = StartCoroutine(ApplyDamageOverTime(dps, duration, maxTotal, "veleno"));
    }

    public void StartFire(float dps, float duration, float maxTotal)
    {
        if (fireCoroutine != null) StopCoroutine(fireCoroutine);
        fireCoroutine = StartCoroutine(ApplyDamageOverTime(dps, duration, maxTotal, "fuoco"));
    }

    private IEnumerator ApplyDamageOverTime(float dps, float duration, float maxTotal, string cause)
    {
        float total = 0f;
        float tick = 1f;
        float elapsed = 0f;
        while (elapsed < duration && total < maxTotal && combatStats.Health > 0)
        {
            float add = Mathf.Min(dps * tick, maxTotal - total);
            combatStats.Health -= add;
            total += add;
            elapsed += tick;
            Debug.Log($"[{cause.ToUpper()} DOT] Inflitti {add} danni, salute attuale: {combatStats.Health}");
            yield return new WaitForSeconds(tick);
        }
    }
}
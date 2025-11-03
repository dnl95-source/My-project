using UnityEngine;
using Game.Combat;

[RequireComponent(typeof(CharacterController))]
public class NPCController : MonoBehaviour
{
    public CharacterAttributes attributes;
    public CombatStats combatStats;
    public MeleeWeapon equippedWeapon;        // Arma da mischia equipaggiata
    public RangedWeapon equippedRangedWeapon;  // Arma a distanza equipaggiata
    public Projectile equippedProjectile;      // Munizione selezionata per arma a distanza

    private NPC npc;
    private CharacterController controller;

    public void Init(NPC npcData, MeleeWeapon meleeWeapon = null, RangedWeapon rangedWeapon = null, Projectile projectile = null)
    {
        npc = npcData;
        attributes = npcData.attributes; // Supponendo che NPC abbia un campo attributes
        combatStats = new CombatStats
        {
            Health = RacialHealthTable.GetHealthByRace(attributes.Race),
            Stamina = 80,
            AttackPower = 15,
            Defense = 8,
            BleedChance = 0.15f,
            DismemberChance = 0.05f
        };
        equippedWeapon = meleeWeapon ?? MeleeWeaponDatabase.Weapons.Find(w => w.Type == WeaponType.Knife);
        equippedRangedWeapon = rangedWeapon;
        equippedProjectile = projectile;
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        // Setup StatusEffectHandler
        var statusHandler = GetComponent<StatusEffectHandler>();
        if (statusHandler != null)
            statusHandler.combatStats = this.combatStats;
    }

    void Update()
    {
        // NPC idle/cammina random (prototipo)
        // Se usi CharacterController per movimento, sostituisci con controller.Move(...)
        transform.position += new Vector3(Mathf.PerlinNoise(Time.time, 0) - 0.5f, 0, Mathf.PerlinNoise(0, Time.time) - 0.5f) * Time.deltaTime;
    }

    // Attacco corpo a corpo
    public void AttackMelee(CharacterControllerBase target, bool isCrit = false)
    {
        if (target == null)
        {
            Debug.LogWarning("AttackMelee: target is null");
            return;
        }

        CombatSystem.Attack(
            this.attributes,
            this.combatStats,
            target.attributes,
            target.combatStats,
            this.equippedWeapon,
            target.gameObject, // necessario per DoT / StatusEffectHandler
            isCrit
        );
    }

    // Attacco a distanza
    public void AttackRanged(CharacterControllerBase target, bool isCrit = false, bool isHeadshot = false)
    {
        if (target == null)
        {
            Debug.LogWarning("AttackRanged: target is null");
            return;
        }

        CombatSystem.RangedAttack(
            this.attributes,
            this.combatStats,
            target.attributes,
            target.combatStats,
            this.equippedRangedWeapon,
            this.equippedProjectile,
            target.gameObject, // <-- necessario per DoT (StatusEffectHandler)
            isCrit,
            isHeadshot
        );
    }
}
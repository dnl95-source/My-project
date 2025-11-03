using UnityEngine;
using Game.Combat;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public CharacterAttributes attributes;
    public CombatStats combatStats;

    // Equipaggiamento
    public MeleeWeapon equippedWeapon; // Arma da mischia equipaggiata
    public RangedWeapon equippedRangedWeapon; // Arma a distanza equipaggiata
    public Projectile equippedProjectile; // Munizione selezionata per arma a distanza

    private CharacterController controller;
    public float speed = 5f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;
    private Vector3 velocity;

    public void Init(CharacterAttributes attr, MeleeWeapon meleeWeapon = null, RangedWeapon rangedWeapon = null, Projectile projectile = null)
    {
        attributes = attr;
        combatStats = new CombatStats
        {
            Health = RacialHealthTable.GetHealthByRace(attr.Race),
            Stamina = 100,
            AttackPower = 20,
            Defense = 10,
            BleedChance = 0.25f,
            DismemberChance = 0.1f
        };
        equippedWeapon = meleeWeapon ?? MeleeWeaponDatabase.Weapons.Find(w => w.Type == WeaponType.Knife);
        equippedRangedWeapon = rangedWeapon;  // Puoi assegnare l'arco o la balestra qui
        equippedProjectile = projectile;      // Puoi assegnare la freccia/dardo qui
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
        // Movimento base stile FPS
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * speed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // Attacco corpo a corpo
    public void AttackMelee(CharacterControllerBase target, bool isCrit = false)
    {
        if (target == null)
        {
            Debug.LogWarning("AttackMelee: target is null");
            return;
        }

        // Passa il GameObject del bersaglio così il CombatSystem può avviare DoT/StatusEffectHandler se necessario
        CombatSystem.Attack(
            this.attributes,
            this.combatStats,
            target.attributes,
            target.combatStats,
            this.equippedWeapon,
            target.gameObject,
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
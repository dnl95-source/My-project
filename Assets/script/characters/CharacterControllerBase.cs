using UnityEngine;
using Game.Combat;

public class CharacterControllerBase : MonoBehaviour
{
    // --- ATTRIBUTI DEL PERSONAGGIO ---
    public CharacterAttributes attributes; // razza, nome, ecc.
    public CombatStats combatStats;        // salute, stamina, difesa, ecc.
    public MeleeWeapon equippedWeapon;     // arma equipaggiata

    // --- MOVIMENTO BASE ---
    public float speed = 5f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;
    private Vector3 velocity;
    private CharacterController controller;

    // --- INIZIALIZZAZIONE ---
    public void Init(CharacterAttributes attr, MeleeWeapon weapon = null)
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
        equippedWeapon = weapon ?? MeleeWeaponDatabase.Weapons.Find(w => w.Type == WeaponType.Knife); // default arma
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Movimento di base stile FPS
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

    // --- ATTACCO ---
    public void Attack(CharacterControllerBase target, bool isCrit = false)
    {
        CombatSystem.Attack(
            this.attributes,
            this.combatStats,
            target.attributes,
            target.combatStats,
            this.equippedWeapon,
            isCrit
        );
    }
}
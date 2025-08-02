using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 40f; // Speed of rotation in degrees per second
    [SerializeField] private ProjectileWeapon[] projectileWeapons; // Array of projectile weapons attached to the boss
    [SerializeField] private float fireCooldownSingle = 1f; // Cooldown time between firing projectiles
    [SerializeField] private float fireCooldownAll = 2f; // Cooldown time between firing all projectiles at once
    private float fireCooldown; // Current fire cooldown based on the fire mode
    private float lastFireTime = 0f; // Time when the boss last fired projectiles
    private int lastSingleWeaponFired = 0;

    private float timeTillChangeFireMode = 10f; // Time until the fire mode changes
    private const float CHANGE_FIRE_MODE_INTERVAL = 10f; // Interval for changing fire mode


    enum FireMode
    {
        Single, // Fire one weapon at a time
        AllAtOnce // Fire all weapons at once
    }

    [SerializeField] private FireMode currentFireMode = FireMode.Single; // Current fire mode of the boss

    private void Start()
    {
        if(FireMode.Single == currentFireMode)
        {
            fireCooldown = fireCooldownSingle; // Set the initial cooldown for single fire mode
        }
        else
        {
            fireCooldown = fireCooldownAll; // Set the initial cooldown for all at once fire mode
        }


    }

    

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime); // Rotate the boss around the Z-axis
        
        if (Time.time >= lastFireTime + fireCooldown)
        {
            Fire(); // Fire projectiles if the cooldown has elapsed
            lastFireTime = Time.time; // Update the last fire time
        }

        // Check if it's time to change the fire mode
        if (Time.time >= timeTillChangeFireMode)
        {
            ChangeFireMode(); // Change the fire mode
            timeTillChangeFireMode += CHANGE_FIRE_MODE_INTERVAL; // Reset the timer for the next change
        }


    }

    private void Fire()
    {
        // Check the current fire mode and fire accordingly
        if (currentFireMode == FireMode.Single)
        {
            FireOneATATime(); // Fire one weapon at a time
        }
        else if (currentFireMode == FireMode.AllAtOnce)
        {
            FireAllAtOnce(); // Fire all weapons at once
        }
    }

    private void ChangeFireMode()
    {
        // Toggle the fire mode
        if (currentFireMode == FireMode.Single)
        {
            currentFireMode = FireMode.AllAtOnce; // Switch to firing all at once
            fireCooldown = fireCooldownAll; // Set the cooldown for firing all at once
        }
        else
        {
            currentFireMode = FireMode.Single; // Switch to firing one at a time
            fireCooldown = fireCooldownSingle; // Set the cooldown for firing one at a time
        }
        // Reset the last single weapon fired index when changing modes
        lastSingleWeaponFired = 0;
    }

    private void FireOneATATime()
    {
        if (projectileWeapons.Length == 0) return; // No weapons to fire
        // Fire the next weapon in the array
        projectileWeapons[lastSingleWeaponFired].Fire();
        
        // Update the index for the next weapon to fire
        lastSingleWeaponFired = (lastSingleWeaponFired + 1) % projectileWeapons.Length;


    }

    private void FireAllAtOnce()
    {
        if (projectileWeapons.Length == 0) return; // No weapons to fire
        // Fire all weapons in the array
        foreach (var weapon in projectileWeapons)
        {
            weapon.Fire();
        }
        // Reset the last single weapon fired index
        lastSingleWeaponFired = 0;

    }
}

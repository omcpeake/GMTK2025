using UnityEngine;

public class ProjectileWeapon : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab; // Prefab for the projectile
    [SerializeField] private bool autoFire = true; // Whether the weapon should fire automatically
    [SerializeField] private Transform firePoint; // Point from where the projectile will be fired
    [SerializeField] private float fireRate = 1f; // Rate of fire in seconds
    private float nextFireTime = 0f; // Time when the next projectile can be fired

    [SerializeField] private AudioClip fireSound; // Sound to play when firing
    [SerializeField] private AudioSource audioSource; // Audio source to play the sound

    // Update is called once per frame
    void Update()
    {     
        if (Time.time >= nextFireTime && autoFire)
        {
            Fire();
        }
    }

    public void Fire()
    {   
        // Instantiate the projectile at the fire point
        GameObject projectileGameObject = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projectile = projectileGameObject.GetComponent<Projectile>();
        if (projectile != null)
        {
            // Set the parent of the projectile to this weapon for organization
            projectile.SetOwner(gameObject);
            if (gameObject.tag == "Player")
            {
                projectile.SetOwnedByPlayer(true);
            }
            else
            {
                projectile.transform.parent = transform; 
            }



        }
        else
        {
            Debug.LogError("Projectile component not found on the projectile prefab.");
        }

        // Play the firing sound
        if (audioSource != null && fireSound != null)
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f); // Randomize pitch for variation
            audioSource.PlayOneShot(fireSound);
        }
        // Update the next fire time
        nextFireTime = Time.time + fireRate;
    }
    
}

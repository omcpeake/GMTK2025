using UnityEngine;

public class ProjectileWeapon : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab; // Prefab for the projectile
    [SerializeField] private Transform firePoint; // Point from where the projectile will be fired
    [SerializeField] private float fireRate = 1f; // Rate of fire in seconds
    private float nextFireTime = 0f; // Time when the next projectile can be fired

    [SerializeField] private AudioClip fireSound; // Sound to play when firing
    [SerializeField] private AudioSource audioSource; // Audio source to play the sound

    // Update is called once per frame
    void Update()
    {     
        if (Time.time >= nextFireTime)
        {
            Fire();
        }
    }

    private void Fire()
    {   
        // Instantiate the projectile at the fire point
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Play the firing sound
        if (audioSource != null && fireSound != null)
        {
            audioSource.PlayOneShot(fireSound);
        }
        // Update the next fire time
        nextFireTime = Time.time + fireRate;
    }
    
}

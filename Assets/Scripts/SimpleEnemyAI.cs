using UnityEngine;


public class SimpleEnemyAI : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void FixedUpdate()
    {
        // always move left and stick to the ground
        transform.Translate(new Vector2(-speed, -1) * Time.deltaTime);




    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if the enemy collides with the player, deal damage
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.TakeDamage(10f); // Deal 10 damage to the player
            }
        }
    }
}

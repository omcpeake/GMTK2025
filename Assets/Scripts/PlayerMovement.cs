using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;
    [Header("Jumping")]
    [SerializeField] private float jumpForce = 10f;
    [Header("GroundCheck")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 groundCheckSize = new Vector2(0.5f, 0.1f);


    private float horizontalInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        /*horizontalInput = context.ReadValue<Vector2>().x;
        Debug.Log(horizontalInput);*/
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && rb != null)
        {
            if(IsGrounded())
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                Debug.Log("Jump performed");
            }
           
        }
        else if (context.canceled)
        {
            if(rb.linearVelocityY > 0)
            {
                // If the jump is canceled while moving upwards, reset the vertical velocity
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0.5f);
                Debug.Log("Jump canceled");
            }
            
        }
    }

    private bool IsGrounded()
    {
        // Check if the player is grounded using a box cast
        return Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
    }

}

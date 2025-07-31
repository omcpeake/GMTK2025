using UnityEngine;
using UnityEngine.InputSystem;

public class RingController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f;
    float horizontalInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, -horizontalInput * rotationSpeed * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalInput = context.ReadValue<Vector2>().x;
        //Debug.Log($"ring Input: {horizontalInput}");
    }
}

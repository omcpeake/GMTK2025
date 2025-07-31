using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]



public class Gate : MonoBehaviour
{
    [SerializeField] int gateNumber;
    private LoopCounter loopCounter;


    private void Awake()
    {
        loopCounter = GetComponentInParent<LoopCounter>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            loopCounter.OnGateEnter(gateNumber, collision);
        }
    }
}

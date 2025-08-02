using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] GameObject objectToRotate;
    [SerializeField] float rotationSpeed = 10f;


    // Update is called once per frame
    void Update()
    {
        objectToRotate.transform.Rotate(new Vector3(0 ,0, rotationSpeed * Time.deltaTime));
    }
}

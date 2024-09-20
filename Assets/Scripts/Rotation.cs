using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Transform target;
    public Vector3 rotationSpeed = new Vector3(0, 100, 0); // Rotation speed in degrees per second

    void Update()
    {
        // Rotate the object around its local axes
        target.Rotate(rotationSpeed * Time.deltaTime);
    }
}
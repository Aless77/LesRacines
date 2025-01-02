using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    // Vitesse de rotation
    public Vector3 rotationSpeed = new Vector3(0, 10, 0);

    void Update()
    {
        // Faire tourner l'objet
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}

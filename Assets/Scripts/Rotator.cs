using UnityEngine;

public class Rotator : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 100f; // Velocidad de rotación en grados por segundo
    public bool rotateClockwise = true; // Dirección de la rotación

    void Update()
    {
        // Si rotateClockwise es true, la rotación será positiva (sentido horario)
        // Si es false, será negativa (sentido antihorario)
        float direction = rotateClockwise ? -1f : 1f;
        
        // Rotamos el objeto usando Euler angles
        transform.Rotate(0f, 0f, direction * rotationSpeed * Time.deltaTime);
    }
} 
using UnityEngine;

public class IsometricCameraFollow : MonoBehaviour
{
    public Transform player; // Obiekt gracza, za którym ma podążać kamera
    public float followSpeed = 2f; // Szybkość, z jaką kamera podąża za graczem
    public Vector3 offset = new Vector3(0, 0, 0); // Offset kamery względem gracza

    private void LateUpdate()
    {
        if (player != null)
        {
            // Docelowa pozycja kamery to pozycja gracza plus offset
            Vector3 targetPosition = player.position + offset;
            // Płynne przesuwanie kamery do docelowej pozycji
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}
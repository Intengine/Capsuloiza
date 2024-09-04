using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    public float orthographicSize = 10f;
    public Vector3 cameraPosition = new Vector3(10, 10, 10); // Pozycja kamery
    public Vector3 cameraRotation = new Vector3(30, 45, 0);  // Kąt patrzenia kamery

    void Start()
    {
        Camera.main.orthographic = true; // Ustawienie kamery na ortograficzną
        Camera.main.orthographicSize = orthographicSize; // Ustawienie rozmiaru ortograficznego
        Camera.main.transform.position = cameraPosition; // Ustawienie pozycji kamery
        Camera.main.transform.rotation = Quaternion.Euler(cameraRotation); // Ustawienie rotacji kamery
    }
}
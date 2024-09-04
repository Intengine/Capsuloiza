using UnityEngine;

public class OrientToCamera : MonoBehaviour
{
    Camera cameraToLookAt;

    void Start()
    {
        cameraToLookAt = Camera.main;
    }

    void LateUpdate()
    {
        transform.LookAt(cameraToLookAt.transform);
        transform.rotation = Quaternion.LookRotation(cameraToLookAt.transform.forward);
    }
}
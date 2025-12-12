using UnityEngine;

public class CameraDetector : MonoBehaviour
{
    public DoorController porta;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Inimigo"))
            porta.SetCameraDetection(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Inimigo"))
            porta.SetCameraDetection(false);
    }
}
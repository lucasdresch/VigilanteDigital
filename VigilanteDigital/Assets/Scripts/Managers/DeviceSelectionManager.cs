using UnityEngine;

public class DeviceSelectionManager : MonoBehaviour
{
    public static DeviceSelectionManager Instance;

    private IDevice selectedDevice;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SelectDevice(IDevice device)
    {
        selectedDevice = device;
        Debug.Log("Dispositivo selecionado: " + device.GetDeviceName());
    }

    public void ClearSelection()
    {
        selectedDevice = null;
        Debug.Log("Nenhum dispositivo selecionado");
    }

    public IDevice GetSelectedDevice()
    {
        return selectedDevice;
    }
}

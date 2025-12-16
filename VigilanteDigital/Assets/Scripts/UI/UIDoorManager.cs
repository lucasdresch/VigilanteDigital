using UnityEngine;

public class UIDoorManager : MonoBehaviour, IDevice
{
    public DoorController door;

    private void OnMouseDown()
    {
        Debug.Log("Porta clicada: " + name);

        // Seleciona no DeviceSelectionManager
        DeviceSelectionManager.Instance.SelectDevice(this);

        // PELO UIDeviceManager, abre o painel
        UIDeviceManager deviceManager = FindObjectOfType<UIDeviceManager>();
        if (deviceManager != null)
        {
            deviceManager.OpenPanelForDevice(this);
        }
    }
    public string GetDeviceName()
    {
        return "Porta";
    }

    public void OnSelected()
    {
        Debug.Log("Porta selecionada");
    }

    public void OnDeselected()
    {
        Debug.Log("Porta desselecionada");
    }

    public DoorController GetDoor()
    {
        return door;
    }

}

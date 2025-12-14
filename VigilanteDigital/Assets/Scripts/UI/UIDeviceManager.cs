using UnityEngine;

public class UIDeviceManager : MonoBehaviour
{
    [Header("Painéis")]
    public GameObject doorConfigPanel;

    private IDevice lastSelectedDevice = null;

    private void Update()
    {
        var selectedDevice = DeviceSelectionManager.Instance.GetSelectedDevice();

        // só reage se a seleção mudou
        if (selectedDevice == lastSelectedDevice)
            return;

        lastSelectedDevice = selectedDevice;

        if (selectedDevice == null)
        {
            Debug.Log("Nenhum dispositivo selecionado");
            CloseAllPanels();
            return;
        }

        Debug.Log("Dispositivo selecionado: " + selectedDevice.GetDeviceName());

        // Porta
        if (selectedDevice is DoorController)
        {
            OpenDoorPanel();
        }
        else
        {
            CloseAllPanels();
        }
    }

    private void OpenDoorPanel()
    {
        if (doorConfigPanel != null)
            doorConfigPanel.SetActive(true);
    }

    private void CloseAllPanels()
    {
        if (doorConfigPanel != null)
            doorConfigPanel.SetActive(false);
    }
}

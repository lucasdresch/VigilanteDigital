using UnityEngine;

public class UIDeviceManager : MonoBehaviour
{
    [Header("Painéis")]
    public GameObject doorConfigPanel;

    private DoorController selectedDoor;

    public DevicePanelUI devicePanelUI;


    /// <summary>
    /// Chame este método ao clicar em um dispositivo
    /// </summary>
    public void OpenPanelForDevice(IDevice device)
    {
        CloseAllPanels();
        Debug.Log($"Tentando ativar painel: {doorConfigPanel} " +
            $"| activeSelf = {doorConfigPanel.activeSelf} " +
            $"| activeInHierarchy = {doorConfigPanel.activeInHierarchy}" +
            $"| idevice = {device.GetDeviceName()}");
        devicePanelUI = doorConfigPanel.GetComponent<DevicePanelUI>();
        if (device is UIDoorManager uiDoor)
        {
            Debug.Log("é idevice");
            selectedDoor = uiDoor.door;
            if (doorConfigPanel != null) { 
                doorConfigPanel.SetActive(true);
                Debug.Log("Abrindo painel: " + doorConfigPanel.name +
                          " | activeSelf = " + doorConfigPanel.activeSelf +
                          " | activeInHierarchy = " + doorConfigPanel.activeInHierarchy);
                if (devicePanelUI != null && selectedDoor != null) {
                    devicePanelUI.deviceNameText.text = selectedDoor.name;
                }
            }
        }
        else {
            selectedDoor = null;
        }
    }
    public void TestActivatePanel()
    {
        Debug.Log($"Testando ativar painel: {doorConfigPanel} " +
                $"| activeSelf = {doorConfigPanel.activeSelf}");
        doorConfigPanel.SetActive(true);
    }
    /// <summary>
    /// Fecha todos os painéis
    /// </summary>
    public void CloseAllPanels()
    {
        Debug.Log("Entrou no close");
        if (doorConfigPanel != null)
        {
            doorConfigPanel.SetActive(false);
            Debug.Log("CloseAllPanels chamado - desativando: " + doorConfigPanel.name +
                      " | activeSelf = " + doorConfigPanel.activeSelf +
                      " | activeInHierarchy = " + doorConfigPanel.activeInHierarchy);
        }
    }

    /// <summary>
    /// Retorna a porta atualmente selecionada
    /// </summary>
    public DoorController GetSelectedDoor()
    {
        return selectedDoor;
    }
}


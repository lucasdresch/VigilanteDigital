using UnityEngine;

public class UIDoorPanel : MonoBehaviour
{
    public UIDeviceManager deviceManager;

    public void ToggleDoor()
    {
        var selected = DeviceSelectionManager.Instance.GetSelectedDevice();
        UIDoorManager doorUI = selected as UIDoorManager;

        if (doorUI == null)
        {
            Debug.LogWarning("Dispositivo selecionado não é uma porta");
            return;
        }

        DoorController door = doorUI.GetDoor();

        if (door == null)
        {
            Debug.LogWarning("Nenhuma porta selecionada");
            return;
        }

        door.ToggleDoor();
        Debug.Log("Botão UI acionou a porta: " + door.name);
    }
}

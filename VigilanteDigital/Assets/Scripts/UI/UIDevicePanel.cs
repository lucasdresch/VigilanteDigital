using TMPro;
using UnityEngine;

public class DevicePanelUI : MonoBehaviour
{
    public static DevicePanelUI Instance;

    [Header("UI")]
    public GameObject panel;
    public TMP_Text deviceNameText;

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void Show(IDevice device)
    {
        panel.SetActive(true);
        deviceNameText.text = device.GetDeviceName();
    }

    public void Hide()
    {
        panel.SetActive(false);
        deviceNameText.text = "";
    }
}

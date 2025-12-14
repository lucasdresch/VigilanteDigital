using UnityEngine;

public class UIDoorManager : MonoBehaviour, IDevice
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        Debug.Log("Porta clicada: " + gameObject.name);
        DeviceSelectionManager.Instance.SelectDevice(this);

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


}

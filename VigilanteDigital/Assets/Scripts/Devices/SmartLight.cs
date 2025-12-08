using UnityEngine;

public class SmartLight : MonoBehaviour, IAutomatable
{
    public Light lightObj;
    public float EnergyCost => 0.05f;

    public void ExecuteAction(string action)
    {
        if (action == $"LightOn_{name}")
            TurnOn();

        if (action == $"LightOff_{name}")
            TurnOff();
    }

    public bool CheckCondition(string condition)
    {
        return AutomationWorldState.HasFlag(condition);
    }

    void TurnOn()
    {
        if (!EnergySystem.Instance.ConsumeEnergy(EnergyCost)) return;
        lightObj.enabled = true;
    }

    void TurnOff()
    {
        lightObj.enabled = false;
    }
}


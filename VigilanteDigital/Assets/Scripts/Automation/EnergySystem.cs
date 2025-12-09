using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    public static EnergySystem Instance;

    public float maxEnergy = 100f;
    public float currentEnergy = 100f;

    void Awake()
    {
        Instance = this;
    }

    public bool ConsumeEnergy(float amount)
    {
        if (currentEnergy < amount)
            return false;

        currentEnergy -= amount;
        return true;
    }
}

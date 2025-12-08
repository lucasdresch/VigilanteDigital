using UnityEngine;

public class MotionSensor : MonoBehaviour, IAutomatable
{
    public bool detected = false;
    public float detectionRadius = 4f;
    public LayerMask enemyMask;

    public float EnergyCost => 0.1f;

    void Update()
    {
        detected = Physics.CheckSphere(transform.position, detectionRadius, enemyMask);

        if (detected)
            AutomationWorldState.SetFlag($"Sensor_{name}_Triggered");
        else
            AutomationWorldState.ClearFlag($"Sensor_{name}_Triggered");
    }

    public void ExecuteAction(string action) { }

    public bool CheckCondition(string condition)
    {
        return AutomationWorldState.HasFlag(condition);
    }
}

using UnityEngine;

public class CameraSpyScript : MonoBehaviour, IAutomatable
{
    public float detectionRange = 10f;
    public LayerMask enemyMask;

    public bool enemyDetected = false;

    public float EnergyCost => 0.2f;

    void Update()
    {
        enemyDetected = Physics.Raycast(transform.position, transform.forward, detectionRange, enemyMask);

        if (enemyDetected)
            AutomationWorldState.SetFlag($"EnemyDetected_{name}");
        else
            AutomationWorldState.ClearFlag($"EnemyDetected_{name}");
    }

    public void ExecuteAction(string action)
    {
        // câmeras geralmente não executam ações
    }

    public bool CheckCondition(string condition)
    {
        return AutomationWorldState.HasFlag(condition);
    }
}

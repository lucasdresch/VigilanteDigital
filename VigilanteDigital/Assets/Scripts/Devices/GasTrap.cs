using UnityEngine;

public class GasTrap : MonoBehaviour, IAutomatable
{
    public float radius = 5f;
    public LayerMask enemyMask;
    public float EnergyCost => 2f;

    public void ExecuteAction(string action)
    {
        if (action == $"ActivateGas_{name}")
            ActivateGas();
    }

    public bool CheckCondition(string condition)
    {
        return AutomationWorldState.HasFlag(condition);
    }

    void ActivateGas()
    {
        if (!EnergySystem.Instance.ConsumeEnergy(EnergyCost)) return;

        Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemyMask);

        foreach (var hit in hits)
        {
            EnemyBase e = hit.GetComponent<EnemyBase>();
            if (e != null)
                e.Sleep(3f);
        }
    }
}

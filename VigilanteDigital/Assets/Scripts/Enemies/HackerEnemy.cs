using UnityEngine;
using System.Collections;

public class HackerEnemy : EnemyBase, IAutomatable
{
    public float hackRange = 3f;

    public float EnergyCost => 0f;

    public void ExecuteAction(string actionName)
    {
        if (actionName == "hack")
            TryHack();
    }

    public bool CheckCondition(string conditionName)
    {
        return false;
    }

    private void TryHack()
    {
        Collider[] results = Physics.OverlapSphere(transform.position, hackRange);

        foreach (var r in results)
        {
            if (r.TryGetComponent<IDesactivatable>(out var target))
            {
                target.Disable();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, hackRange);
    }
}

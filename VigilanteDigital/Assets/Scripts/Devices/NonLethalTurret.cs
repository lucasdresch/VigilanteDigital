using UnityEngine;

public class NonLethalTurret : MonoBehaviour, IAutomatable
{
    public float range = 10f;
    public LayerMask enemyMask;

    public float fireCooldown = 1f;
    private float cooldownTimer = 0f;

    public float EnergyCost => 1.5f;

    void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public void ExecuteAction(string action)
    {
        if (action == $"Fire_{name}")
            TryFire();
    }

    public bool CheckCondition(string condition)
    {
        return AutomationWorldState.HasFlag(condition);
    }

    private void TryFire()
    {
        if (cooldownTimer > 0) return;
        if (!EnergySystem.Instance.ConsumeEnergy(EnergyCost)) return;

        Collider[] hits = Physics.OverlapSphere(transform.position, range, enemyMask);

        foreach (var hit in hits)
        {
            EnemyBase e = hit.GetComponent<EnemyBase>();
            if (e != null)
                e.Stun(2f);
        }

        cooldownTimer = fireCooldown;
    }
}

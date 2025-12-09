using UnityEngine;

public class AutoDoor : MonoBehaviour, IAutomatable
{
    public bool isOpen = false;
    public float EnergyCost => 0.5f;

    public Animator animator;

    public void ExecuteAction(string action)
    {
        if (action == $"Open_{name}")
            Open();

        if (action == $"Close_{name}")
            Close();
    }

    public bool CheckCondition(string condition)
    {
        // Exemplo: "EnemyNear_DoorA"
        return AutomationWorldState.HasFlag(condition);
    }

    public void Open()
    {
        if (!EnergySystem.Instance.ConsumeEnergy(EnergyCost)) return;

        isOpen = true;
        animator.SetBool("Open", true);
    }

    public void Close()
    {
        if (!EnergySystem.Instance.ConsumeEnergy(EnergyCost)) return;

        isOpen = false;
        animator.SetBool("Open", false);
    }
}


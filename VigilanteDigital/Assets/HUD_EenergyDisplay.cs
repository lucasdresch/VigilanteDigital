using UnityEngine;
using TMPro;

public class HUD_EnergyDisplay : MonoBehaviour
{
    public TMP_Text energyText;

    private void Update()
    {
        float energy = GameManager.Instance.currentEnergy;
        energyText.text = ((int)energy).ToString();
    }
}

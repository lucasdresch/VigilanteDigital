using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public TextMeshProUGUI playerLifeText;
    public TextMeshProUGUI enemiesLeftText;
    public TextMeshProUGUI energyText;

    void Update()
    {
        if (GameManager.Instance != null)
        {
            playerLifeText.text = $"Vida: {GameManager.Instance.playerLife}/3";
            enemiesLeftText.text = $"Inimigos: {GameManager.Instance.enemiesInScene.Count}";
        }
        float energy = GameManager.Instance.currentEnergy;
        float max = GameManager.Instance.maxEnergy;

        // Atualiza o texto
        energyText.text = $"{energy}%";

        // Aplica a cor dinâmica
        energyText.color = GetEnergyColor(energy, max);
    }

    private Color32 GetEnergyColor(float energy, float max)
    {
        float p = Mathf.Clamp01(energy / max); // porcentagem 0..1

        // 100% → 75% (Azul → Ciano)
        if (p > 0.75f)
        {
            float t = Mathf.InverseLerp(1f, 0.75f, p); // 1→0
            byte g = (byte)Mathf.Lerp(0, 255, t);
            return new Color32(0, g, 255, 255);
        }

        // 75% → 50% (Ciano → Verde)
        if (p > 0.50f)
        {
            float t = Mathf.InverseLerp(0.75f, 0.50f, p);
            byte b = (byte)Mathf.Lerp(255, 0, t);
            return new Color32(0, 255, b, 255);
        }

        // 50% → 25% (Verde → Amarelo)
        if (p > 0.25f)
        {
            float t = Mathf.InverseLerp(0.50f, 0.25f, p);
            byte r = (byte)Mathf.Lerp(0, 255, t);
            return new Color32(r, 255, 0, 255);
        }

        // 25% → 0% (Amarelo → Vermelho)
        {
            float t = Mathf.InverseLerp(0.25f, 0f, p);
            byte g = (byte)Mathf.Lerp(255, 0, t);
            return new Color32(255, g, 0, 255);
        }
    }

    public void RestartScene()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }

    public void StartEnemies()
    {
        if (GameManager.Instance == null) return;

        foreach (var enemy in GameManager.Instance.enemiesInScene)
        {
            if (enemy != null)
                enemy.StartFollowing();
        }
    }

    public GameObject painel;   // o painel com seus botões
    public GameObject menuButton; // botão que reaparece quando o painel some

    public void HidePanel()
    {
        painel.SetActive(false);
        menuButton.SetActive(true);
    }

    public void ShowPanel()
    {
        painel.SetActive(true);
        menuButton.SetActive(false);
    }
}

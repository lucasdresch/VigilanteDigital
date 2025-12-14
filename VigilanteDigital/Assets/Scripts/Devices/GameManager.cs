using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    [Header("Configurações do jogo")]
    public int maxPlayerLife = 3;
    public int playerLife;

    [Header("Enemies da cena")]
    public List<EnemyAI_Node> enemiesInScene = new List<EnemyAI_Node>();

    [Header("Nodes")]
    public Node exitNode; // node de saída, caso precise

    [Header("Estado do jogo")]
    public bool gameOver = false;
    public bool gameWon = false;

    // ENERGIA ATUAL DO JOGADOR
    public float maxEnergy = 100;
    public float currentEnergy = 100;

    public void AddEnergy(float amount)
    {
        currentEnergy += amount;
        if (currentEnergy > maxEnergy)
            currentEnergy = maxEnergy;
    }
    public bool ConsumeEnergy(float amount)
    {
        if (currentEnergy >= amount)
        {
            currentEnergy -= amount;
            return true;
        }

        return false; // sem energia suficiente
    }
    private void Start()
    {
        playerLife = maxPlayerLife;
        CheckEnemiesList();
    }

    // ---------------------------------------------------
    // Player recebe dano
    // ---------------------------------------------------
    public void PlayerTakeDamage(int amount)
    {
        if (gameOver) return;

        playerLife -= amount;
        Debug.Log($"Player tomou {amount} de dano. Vida restante: {playerLife}");

        if (playerLife <= 0)
            EndGame(false);
    }

    // ---------------------------------------------------
    // Adiciona inimigo à lista
    // ---------------------------------------------------
    public void RegisterEnemy(EnemyAI_Node enemy)
    {
        if (!enemiesInScene.Contains(enemy))
            enemiesInScene.Add(enemy);
    }

    // ---------------------------------------------------
    // Remove inimigo da lista
    // ---------------------------------------------------
    public void UnregisterEnemy(EnemyAI_Node enemy)
    {
        if (enemiesInScene.Contains(enemy))
            enemiesInScene.Remove(enemy);

        CheckEnemiesList();
    }

    // ---------------------------------------------------
    // Checa se todos os inimigos morreram
    // ---------------------------------------------------
    private void CheckEnemiesList()
    {
        if (enemiesInScene.Count == 0 && !gameOver)
        {
            EndGame(true);
        }
    }

    // ---------------------------------------------------
    // Finaliza o jogo
    // ---------------------------------------------------
    private void EndGame(bool won)
    {
        gameOver = true;
        gameWon = won;

        if (won)
            Debug.Log("Jogador venceu! Todos os inimigos eliminados.");
        else
            Debug.Log("Game Over! Player perdeu todas as vidas.");
    }
}

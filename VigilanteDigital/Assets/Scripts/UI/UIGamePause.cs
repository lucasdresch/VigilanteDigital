using UnityEngine;

public class UIGamePause : MonoBehaviour
{
    public GameObject pauseButton;       // referência ao botão Pause
    public GameObject continueButton;    // referência ao botão Continue

    private bool isPaused = false;

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;

        if (pauseButton != null) pauseButton.SetActive(false);
        if (continueButton != null) continueButton.SetActive(true);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
        isPaused = false;

        if (continueButton != null) continueButton.SetActive(false);
        if (pauseButton != null) pauseButton.SetActive(true);
    }
}


using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject gameOverPanel;
    public GameObject pauseButton;

    void Start()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("No se encontró el GameManager en la escena.");
            return;
        }
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDestroy()
    {
        // Desuscribirse para evitar errores cuando se recargue la escena
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }

    // Update is called once per frame
    public void PausedGame()
    {
        GameManager.Instance.PauseGame();
    }

    public void ResumeGame()
    {
        GameManager.Instance.ResumeGame();
    }

    public void EndGame()
    {
        GameManager.Instance.ReturnToMainMenu();
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }

    public void GameOver()
    {
        GameManager.Instance.GameOver();
    }

    void HandleGameStateChanged(GameManager.GameState newState)
    {
        if (gameOverPanel != null)
        {
            // Activa el panel solo si el estado es GameOver
            gameOverPanel.SetActive(newState == GameManager.GameState.GameOver);

        }
        if (winPanel != null)
        {
            // Activa el panel solo si el estado es Win
            winPanel.SetActive(newState == GameManager.GameState.Win);

        }
        if (pauseButton != null)
        {
            // Desactiva el botón solo si el estado es GameOver
            pauseButton.SetActive(newState != GameManager.GameState.GameOver);
        }
    }
}

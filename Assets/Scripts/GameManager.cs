using UnityEngine;
using UnityEngine.SceneManagement;
using System; // Necesario para los eventos

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState { MainMenu, Playing, Paused, GameOver, Win }
    public GameState CurrentState { get; private set; }
     public static event Action<GameState> OnGameStateChanged; // Evento

    private void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); }
    }

    private void Start() { ChangeState(GameState.MainMenu); }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
        OnGameStateChanged?.Invoke(newState); // Dispara el evento
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
        ChangeState(GameState.Playing);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        ChangeState(GameState.Paused);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        ChangeState(GameState.Playing);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        ChangeState(GameState.MainMenu);
    }

    public void GameOver()
    {   
        Time.timeScale = 0f;
        ChangeState(GameState.GameOver);
    }

    public void WinGame()
    {   
        Time.timeScale = 0f;
        ChangeState(GameState.Win);
    }
}
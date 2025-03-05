using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject panelPauseMenu; // Asigna el panel de pausa en el Inspector
    public GameObject pauseButton;    // Asigna el botón de pausa en el Inspector
    private bool isPaused = false;

    void Start()
    {
        panelPauseMenu.SetActive(false); // Asegura que el panel esté oculto al inicio
    }

    public void PauseGame()
    {
        isPaused = true;
        panelPauseMenu.SetActive(true); // Muestra el panel de pausa
        pauseButton.SetActive(false);   // Oculta el botón de pausa
        Time.timeScale = 0; // Pausa el tiempo del juego
    }

    public void ResumeGame()
    {
        isPaused = false;
        panelPauseMenu.SetActive(false); // Oculta el panel de pausa
        pauseButton.SetActive(true);     // Muestra el botón de pausa
        Time.timeScale = 1; // Reanuda el tiempo del juego
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Asegura que el tiempo vuelva a la normalidad
        SceneManager.LoadScene("SampleScene"); // Reinicia la escena
    }

    public void ExitGame()
    {
        Time.timeScale = 1; // Evita que el juego se quede pausado
        SceneManager.LoadScene("MainMenu"); // Vuelve al menú principal
    }

    public void PlayGame()
    {
        Time.timeScale = 1; // Asegura que el tiempo se reanude
        SceneManager.LoadScene("SampleScene"); // Inicia el juego en SampleScene
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1; // Asegura que el tiempo no esté pausado
        SceneManager.LoadScene("MainMenu"); // Va al menú principal
    }
}

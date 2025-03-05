using UnityEngine;
using UnityEngine.SceneManagement; // Importa SceneManager

public class PauseController : MonoBehaviour
{
    public GameObject panelPauseMenu; // Arrastra el PanelPauseMenu en el Inspector

    private bool isPaused = false;

    void Start()
    {
        panelPauseMenu.SetActive(false); // Asegura que el panel esté oculto al inicio
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        panelPauseMenu.SetActive(true); // Muestra el panel de pausa
        Time.timeScale = 0; // Pausa el juego
    }

    public void ResumeGame()
    {
        isPaused = false;
        panelPauseMenu.SetActive(false); // Oculta el panel de pausa
        Time.timeScale = 1; // Reanuda el juego
    }

      public void ReturnToSampleScene()
    {
        Time.timeScale = 1; // Asegura que el tiempo vuelva a la normalidad
        SceneManager.LoadScene("SampleScene"); // Carga SampleScene
    }

        public void GoToMainMenu()
    {
        Time.timeScale = 1; // Asegúrate de reanudar el tiempo antes de cambiar de escena
        SceneManager.LoadScene("mainmenu"); // Cambia "mainmenu" por el nombre exacto de tu escena
    }
}

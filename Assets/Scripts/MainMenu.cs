using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Referencias a los botones
    public Button playButton;
    public Button optionsButton;
    public Button exitButton;
    public Button regresarButton;  // Botón para regresar desde opciones

    // Referencias a los paneles
    public GameObject mainPanel;
    public GameObject optionPanel;

    private void Awake()
    {
        // Verificar que los botones estén correctamente asignados
        if (playButton != null)
        {
            playButton.onClick.AddListener(PlayButtonClicked);
        }
        else
        {
            Debug.LogError("El botón Play no está asignado.");
        }

        if (optionsButton != null)
        {
            optionsButton.onClick.AddListener(OptionsButtonClicked);
        }
        else
        {
            Debug.LogError("El botón Options no está asignado.");
        }

        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitButtonClicked);
        }
        else
        {
            Debug.LogError("El botón Exit no está asignado.");
        }

        if (regresarButton != null)
        {
            regresarButton.onClick.AddListener(RegresarButtonClicked);
        }
        else
        {
            Debug.LogError("El botón Regresar no está asignado.");
        }
    }

    // Función al hacer clic en el botón Play
    public void PlayButtonClicked()
    {
        Debug.Log("Cargando el juego...");
        SceneManager.LoadScene("SampleScene");  // Cambiar "Level1" por tu escena real
    }

    // Función al hacer clic en el botón Options
    public void OptionsButtonClicked()
    {
        Debug.Log("Abriendo opciones...");
        mainPanel.SetActive(false);
        optionPanel.SetActive(true);
    }

    // Función al hacer clic en el botón Exit
    public void ExitButtonClicked()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();  // Cierra el juego
    }

    // Función al hacer clic en el botón Regresar
    public void RegresarButtonClicked()
    {
        Debug.Log("Regresando al menú principal...");
        optionPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
}


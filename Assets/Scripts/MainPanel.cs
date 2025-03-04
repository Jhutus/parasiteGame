using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainPanel : MonoBehaviour
{
    [Header("Options")]
    public Slider volumenfx;
    public Slider master;
    public Toggle mute;
    public AudioMixer audioMixer;

    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject optionPanel;
    public GameObject playPanel;  // Panel de play
    public GameObject levelSelectPanel;
    public GameObject exitPanel;

    [Header("Buttons")]
    public Button playButton;
    public Button optionsButton;
    public Button exitButton;
    public Button enterButton;
    public Button backButtonPlay;  // BackButton en el panel de play
    public Button backButtonOptions;  // BackButton en el panel de opciones

    [Header("Level Buttons")]
    public Button level1Button;
    public Button level2Button;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip clickSound;

    private void Awake()
    {
        // Asignar eventos a los botones
        playButton.onClick.AddListener(() => PlayButtonClicked());
        optionsButton.onClick.AddListener(() => OptionsButtonClicked());
        exitButton.onClick.AddListener(() => ExitButtonClicked());
        enterButton.onClick.AddListener(() => EnterButtonClicked());
        backButtonPlay.onClick.AddListener(() => BackButtonClickedFromPlay()); // BackButton desde el panel de Play
        backButtonOptions.onClick.AddListener(() => BackButtonClickedFromOptions()); // BackButton desde el panel de Opciones

        // Asignar eventos de los botones de niveles
        level1Button.onClick.AddListener(() => LoadLevel(1));
        level2Button.onClick.AddListener(() => LoadLevel(2));

        // Habilitar el botón Ingresar inicialmente
        enterButton.interactable = false;
    }

    private void PlayClickSound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
        else
        {
            Debug.LogError("AudioSource o AudioClip de clic no están asignados.");
        }
    }

    private void PlayButtonClicked()
    {
        PlayClickSound();
        OpenPanel(levelSelectPanel);
    }

    private void OptionsButtonClicked()
    {
        PlayClickSound();
        OpenPanel(optionPanel);
    }

    private void ExitButtonClicked()
    {
        PlayClickSound();
        OpenExitPanel(); // Mostrar panel de confirmación de salida
    }

    private void EnterButtonClicked()
    {
        PlayClickSound();
        OpenPanel(mainPanel); // Regresar al panel principal
    }

    private void BackButtonClickedFromPlay()
    {
        PlayClickSound();
        OpenPanel(mainPanel); // Regresar al panel principal desde el panel de Play
    }

    private void BackButtonClickedFromOptions()
    {
        PlayClickSound();
        OpenPanel(mainPanel); // Regresar al panel principal desde el panel de Opciones
    }

    private void OpenExitPanel()
    {
        mainPanel.SetActive(false);
        exitPanel.SetActive(true); // Mostrar el panel de salida
        enterButton.interactable = true; // Habilitar el botón Ingresar
    }

    public void OpenPanel(GameObject panel)
    {
        mainPanel.SetActive(false);
        optionPanel.SetActive(false);
        playPanel.SetActive(false); // Asegurarse de ocultar el panel de Play
        levelSelectPanel.SetActive(false);
        exitPanel.SetActive(false); // Asegurarse de ocultar el panel de salida

        panel.SetActive(true);
    }

    public void ChangeMasterVolume(float volume)
    {
        if (audioMixer != null)
        {
            float dB = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20;
            audioMixer.SetFloat("VolMaster", dB);
        }
    }

    public void ChangeFXVolume(float volume)
    {
        if (audioMixer != null)
        {
            float dB = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20;
            audioMixer.SetFloat("VolFX", dB);
        }
    }

    public void ToggleMute(bool isMuted)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("VolMaster", isMuted ? -80f : Mathf.Log10(master.value) * 20);
        }
    }

    public void LoadLevel(int levelIndex)
    {
        PlayClickSound();
        SceneManager.LoadScene(levelIndex);
    }

    public void Exit()
    {
        Debug.Log("Saliendo del Juego");
        Application.Quit();
    }
}

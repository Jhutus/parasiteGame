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
    public GameObject playPanel;  
    public GameObject levelSelectPanel;
    public GameObject exitPanel;

    [Header("Buttons")]
    public Button playButton;
    public Button optionsButton;
    public Button exitButton;
    public Button enterButton;
    public Button backButtonPlay;  
    public Button backButtonOptions;  
    public Button restartButton;  // Nuevo botón para reiniciar
    public Button returnButton;   // Nuevo botón para volver al MainPanel

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
        backButtonPlay.onClick.AddListener(() => BackButtonClickedFromPlay()); 
        backButtonOptions.onClick.AddListener(() => BackButtonClickedFromOptions()); 
        level1Button.onClick.AddListener(() => LoadLevel(1));
        level2Button.onClick.AddListener(() => LoadLevel(2));

        // Asignar eventos de los nuevos botones
        if (restartButton != null) restartButton.onClick.AddListener(() => RestartGame());
        if (returnButton != null) returnButton.onClick.AddListener(() => ReturnToMainPanel());
        if (exitButton != null) exitButton.onClick.AddListener(() => ExitGame());

        enterButton.interactable = false;

        // Asignar evento de mute
        mute.onValueChanged.AddListener(ToggleMute);
        
        // Cargar estado de mute guardado
        LoadMuteState();
    }

    public void PlayClickSound()
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
        OpenExitPanel();
    }

    private void EnterButtonClicked()
    {
        PlayClickSound();
        OpenPanel(mainPanel);
    }

    private void BackButtonClickedFromPlay()
    {
        PlayClickSound();
        OpenPanel(mainPanel);
    }

    private void BackButtonClickedFromOptions()
    {
        PlayClickSound();
        OpenPanel(mainPanel);
    }

    private void OpenExitPanel()
    {
        mainPanel.SetActive(false);
        exitPanel.SetActive(true);
        enterButton.interactable = true;
    }

    public void OpenPanel(GameObject panel)
    {
        mainPanel.SetActive(false);
        optionPanel.SetActive(false);
        playPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
        exitPanel.SetActive(false);

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
            PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    private void LoadMuteState()
    {
        bool isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
        mute.isOn = isMuted; // Actualiza el estado del Toggle en la UI
        ToggleMute(isMuted); // Aplica el mute al AudioMixer
    }

    public void LoadLevel(int levelIndex)
    {
        PlayClickSound();
        SceneManager.LoadScene(levelIndex);
    }

    // Función para reiniciar el juego en SampleScene
    public void RestartGame()
    {
        PlayClickSound();
        SceneManager.LoadScene("SampleScene");
    }

    // Función para volver al MainPanel
    public void ReturnToMainPanel()
    {
        PlayClickSound();
        OpenPanel(mainPanel);
    }

    // Función para salir del juego
    public void ExitGame()
    {
        PlayClickSound();
        Debug.Log("Saliendo del Juego");
        Application.Quit();
    }
}

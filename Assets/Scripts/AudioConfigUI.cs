using UnityEngine;
using UnityEngine.UI;

public class AudioConfigUI : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;
    public Toggle muteToggle;

    private void Start()
    {
        // Esperar a que el AudioManager se haya cargado
        if (AudioManager.Instance == null)
        {
            Debug.LogError("No se encontró el AudioManager en la escena.");
            return;
        }

        // Cargar valores almacenados en PlayerPrefs
        if (musicSlider != null)
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicSlider", 1f);
            musicSlider.onValueChanged.AddListener(value => AudioManager.Instance.SetMusicVolume(value));
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = PlayerPrefs.GetFloat("SFXSlider", 1f);
            sfxSlider.onValueChanged.AddListener(value => AudioManager.Instance.SetSFXVolume(value));
        }

        if (muteToggle != null)
        {
            muteToggle.isOn = PlayerPrefs.GetInt("IsMuted", 0) == 1;
            muteToggle.onValueChanged.AddListener(value => AudioManager.Instance.SetMute(value));
        }
    }
}
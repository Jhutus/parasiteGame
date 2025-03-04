using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MuteController : MonoBehaviour
{
    public AudioMixer audioMixer;  // Arrastrar el Audio Mixer en el Inspector
    public Toggle toggleMute;      // Arrastrar el Toggle en el Inspector
    private bool isMuted = false;

    void Start()
    {
        // Cargar estado guardado
        isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
        toggleMute.isOn = isMuted;
        ApplyMute();

        // Detectar cambios en el toggle
        toggleMute.onValueChanged.AddListener(delegate { ToggleMute(); });
    }

    public void ToggleMute()
    {
        isMuted = toggleMute.isOn;
        ApplyMute();
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void ApplyMute()
    {
        if (isMuted)
            audioMixer.SetFloat("MasterVolume", -80f); // Mutear (bajar volumen)
        else
            audioMixer.SetFloat("MasterVolume", 0f);   // Volver al volumen normal
    }
}


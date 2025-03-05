using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    private AudioSource sound;
    public AudioClip ClickAudio;
    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    public void ClickButtonSound()
    {
        sound.PlayOneShot(ClickAudio);
    }
}

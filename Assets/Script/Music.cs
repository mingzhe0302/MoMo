using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public AudioSource audioSource;
    public Sprite musicOnIcon;
    public Sprite musicOffIcon;
    private Image musicIcon;
    private bool isMusicOn = true;

    private void Start()
    {
        musicIcon = GetComponent<Image>();

        // Initialize the state
        ToggleMusic();
    }

    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;

        if (isMusicOn)
        {
            audioSource.Play(); // Start playing the music
            musicIcon.sprite = musicOnIcon; // Set the icon to "Music On"
        }
        else
        {
            audioSource.Stop(); // Stop playing the music
            musicIcon.sprite = musicOffIcon; // Set the icon to "Music Off"
        }
    }
}

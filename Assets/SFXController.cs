using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider = null;

    [SerializeField] private Text volumeTextUI = null;

    private void Start()
    {
        LoadValues();
    }

    public void VolumeSlider(float volume) => volumeTextUI.text = volume.ToString("0.0");

    public void SaveVolumeButton()
    {
        float volumeValue = volumeSlider.value;
        PlayerPrefs.SetFloat("MusicValue", volumeValue);
        LoadValues();

    }

    void LoadValues()
    {
        float volumeValue = PlayerPrefs.GetFloat("MusicValue");
        volumeSlider.value = volumeValue;
        AudioListener.volume = volumeValue;
    }

}

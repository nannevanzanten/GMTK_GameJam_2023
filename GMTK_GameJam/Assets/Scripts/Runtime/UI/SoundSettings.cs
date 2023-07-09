using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] Slider soundSlider;
    [SerializeField] AudioSource audioSource;

    private void Start()
    {
        SetVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 100));
    }

    public void SetVolume(float _value)
    {
        RefreshSlider(_value);

        PlayerPrefs.SetFloat("SavedMasterVlolume", _value);
        audioSource.volume = _value / 100;
    }

    public void UpdateVolume()
    {
        float newVolume = soundSlider.value;
        SetVolume(newVolume);
    }

    public void SetVolumeFromSlider()
    {
        SetVolume(soundSlider.value);   
    }

    public void RefreshSlider(float _value)
    {
        soundSlider.value = _value;
    }
}

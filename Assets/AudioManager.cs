using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;
    public Slider sfxSlider;

    void Start() {
        slider.value = PlayerPrefs.GetFloat("Music", 0.75f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFX", 0.75f);
    }

    public void SetLevel(float value) {
        value = slider.value;
        PlayerPrefs.SetFloat("Music", value);
        mixer.SetFloat("Music", Mathf.Log10(value) * 20);
    }

    public void SetAudioLevel() {
        float value = sfxSlider.value;
        PlayerPrefs.SetFloat("SFX", value);
        mixer.SetFloat("SFX", Mathf.Log10(value) * 20);
    }
}

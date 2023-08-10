using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuController : MonoBehaviour
{
    public AudioMixer mixer;

    void Start() {
        float masterVolume = PlayerPrefs.GetFloat("Music");
        mixer.SetFloat("Music", Mathf.Log10(masterVolume) * 20);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void StartGame() {
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadSettings() {
        SceneManager.LoadScene("Settings");
    }

    public void LoadMenu() {
        SceneManager.LoadScene("Menu");
    }
}

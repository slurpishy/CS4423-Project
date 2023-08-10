using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class AudioFade
{
    public static IEnumerator FadeOutAudio(AudioSource audio, float FadeTime) {
        float volume = audio.volume;
        while (audio.volume > 0) {
            audio.volume -= volume * Time.deltaTime / FadeTime;
            yield return null;
        }
        audio.Stop();
        audio.volume = volume;
    }

    public static IEnumerator FadeInAudio(AudioSource audio, float FadeTime)
    {
        float volume = 0.34f;
        audio.volume = 0;
        audio.Play();
        while (audio.volume < 1.0f) {
            audio.volume += volume * Time.deltaTime / FadeTime;
            yield return null;
        }
        audio.volume = 1.0f;
    }
}


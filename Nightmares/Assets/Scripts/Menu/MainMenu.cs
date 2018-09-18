using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject loadingScreen;

    public Slider slider;

    public AudioMixer audioMixer;

    public GameObject bMusic;

    private void Awake()
    {
        DontDestroyOnLoad(bMusic);

    }

    public void Play() {
        StartCoroutine(LoadLevel());
    }

    public void Quit() {
        EditorApplication.isPlaying = false;
    }

    IEnumerator LoadLevel() {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        loadingScreen.SetActive(true);

        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress) / 0.9f;
            slider.value = progress;
            yield return null;
        }
    }

    public void SetVolume(float volume) {
        audioMixer.SetFloat("masterVol", volume);
    }
}

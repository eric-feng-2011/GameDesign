using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool gamePaused = false;

    public GameObject pauseMenuUI;

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P)) {
            if (gamePaused) {
                Resume();
            } else {
                Pause();
            }
        }
	}

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void Menu() {
        Time.timeScale = 1f;
        gamePaused = false;
        SceneManager.LoadScene(0);
    }

    public void Quit() {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        gamePaused = true;
    }
}

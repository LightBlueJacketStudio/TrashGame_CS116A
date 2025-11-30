using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public GameObject pauseUI, menuUI;

    public void RestartPress() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void ResumePress() {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void ExitPress() {
        Application.Quit();
    }

    public void PausePress() {
        pauseUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void StartGame() {
        menuUI.SetActive(false);
        Time.timeScale = 1;
    }
}

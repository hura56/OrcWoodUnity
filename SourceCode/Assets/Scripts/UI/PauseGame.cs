using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;

    private bool isGamePaused = false;

    void Update()
    {
        if (Input.GetKeyUp(pauseKey))
        {
            if (!isGamePaused)
            {
                Pause();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void Pause()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}

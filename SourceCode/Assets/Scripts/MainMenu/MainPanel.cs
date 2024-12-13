using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainPanel : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI highTimeText;

    private void Start()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        float highTime = PlayerPrefs.GetFloat("HighTime", 0);
        int minutes = Mathf.FloorToInt(highTime / 60);
        int seconds = Mathf.FloorToInt(highTime % 60);
        highScoreText.text = "High Score: " + highScore;
        highTimeText.text = $"Longest Game: {minutes} minutes and {seconds} seconds";
    }

    public void SettingsPanelActivate()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    public void MainPanelActivate()
    {
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void CreditsPanelActivate()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void ExitButtonClick()
    {
        Application.Quit();
    }

    // Metoda do ³adowania sceny o nazwie
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void StartButtonClick()
    {
        LoadSceneByName("Gameplay");
    }
}

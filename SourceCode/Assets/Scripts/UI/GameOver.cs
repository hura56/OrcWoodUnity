using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lastScoreText;
    [SerializeField] private TextMeshProUGUI lastTimeText;

    private void Start()
    {
        int lastScore = PlayerPrefs.GetInt("LastScore", 0);
        float lastTime = PlayerPrefs.GetFloat("LastTime", 0);
        int minutes = Mathf.FloorToInt(lastTime / 60);
        int seconds = Mathf.FloorToInt(lastTime % 60);
        lastScoreText.text = "Score: " + lastScore;
        lastTimeText.text = $"Time survived: {minutes} minutes and {seconds} seconds";
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}

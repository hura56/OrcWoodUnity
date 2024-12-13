using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public PlayerController playerController;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    private float secondsSurvived = 0f;
    private int minutesSurvived = 0;

    void Update()
    {
        secondsSurvived += Time.deltaTime;
        if (secondsSurvived >= 60f)
        {
            minutesSurvived += 1;
            secondsSurvived = 0f;
        }
        scoreText.text = "Score: " + playerController.playerScore.ToString();
        timeText.text = $"Time survived: {minutesSurvived}m {secondsSurvived.ToString("F0")}s";
    }
}

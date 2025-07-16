using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI currentScoreText;

    private int currentScore = 0;
    private int highScore = 0;


    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore.ToString();
        StartCoroutine(CountScore());   
    }

    private void Update()
    {
        HandleScore();
    }

    void HandleScore()
    {
        currentScoreText.text = "Score: " + currentScore.ToString();
        if(currentScore > highScore)
        {
            highScore = currentScore;
            highScoreText.text = "HighScore: " + highScore.ToString();

            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
    }
    
    IEnumerator CountScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            currentScore++;
        }

    }
}

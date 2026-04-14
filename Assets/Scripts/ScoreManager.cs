using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;
    [SerializeField]
    private Text scoreText;
    public int Score => score;
    public void InitializeScore()
    {
        score = 0;
        UpdateScoreText();
    }
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }
    private void UpdateScoreText()
    {
        scoreText.text = "Score\n " + score.ToString();
    }

}

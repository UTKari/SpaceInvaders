using UnityEngine;

public class BestScore : MonoBehaviour
{
    [SerializeField]
    private TextMesh bestScoreText;
    public void UpdateBestScore(int bestScore)
    {
        int highScore = PlayerPrefs.GetInt("BestScore", 0);
        if (bestScore > highScore)
        {
            PlayerPrefs.SetInt("BestScore", bestScore);
            bestScoreText.text = bestScore.ToString();
        }
        else
        {
            bestScoreText.text = highScore.ToString();
        }
    }
}

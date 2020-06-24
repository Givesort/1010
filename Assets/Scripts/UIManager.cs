using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameState gameState;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;


    private void Update()
    {
        scoreText.text = gameState.GetScore().ToString();
        highScoreText.text = gameState.GetHighScore().ToString();
    }
}

using TMPro;
using UnityEngine;

public class PointsUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI pointsText;

    public void SetText(string text) {
        pointsText.text = text;
    }

    public void CheckForNewHighScore(int score) {
        HighScoreManager.TrySetNewHighScore(score);
    }
    public class GameLogic : MonoBehaviour {
    [SerializeField] private PointsUI pointsUI;
    private int playerScore;

    private void EndGame() {
        pointsUI.CheckForNewHighScore(playerScore);
    }
  }
}
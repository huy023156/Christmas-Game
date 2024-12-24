using TMPro;
using UnityEngine;

public class HighScoreDisplay : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI highScoreText;

    private void Start() {
        UpdateHighScoreText();
    }

    public void UpdateHighScoreText() {
        highScoreText.text = "High Score: " + HighScoreManager.HighScore;
    }
}
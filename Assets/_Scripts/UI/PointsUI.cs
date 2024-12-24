using System;
using TMPro;
using UnityEngine;

public class PointsUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI pointsText;

    private void OnEnable() {
        EventDispatcher.Add<EventDefine.OnLoseGame>(OnLoseGame);
    }

    private void OnDisable() {
        EventDispatcher.Remove<EventDefine.OnLoseGame>(OnLoseGame);
    }

    public void SetText(string text) {
        pointsText.text = text;
    }

    public void CheckForNewHighScore(int score) {
        HighScoreManager.TrySetNewHighScore(score);
    }

    private void OnLoseGame(IEventParam param)
    {
        CheckForNewHighScore(GameManager.Instance.CurrentPoints);
    }
}
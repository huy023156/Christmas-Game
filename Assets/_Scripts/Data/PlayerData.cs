using UnityEngine;

public static class PlayerData {
    private const string HighScoreKey = "HighScore";

    public static int HighScore {
        get => PlayerPrefs.GetInt(HighScoreKey, 0);
        private set => PlayerPrefs.SetInt(HighScoreKey, value);
    }

    public static void TrySetNewHighScore(int score) {
        if (score > HighScore) {
            HighScore = score;
        }
    }
}
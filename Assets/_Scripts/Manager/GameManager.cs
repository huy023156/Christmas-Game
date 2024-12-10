using UnityEngine;

public class GameManager : Singleton<GameManager> {
    private int currentLevel;
    private LevelData currentLevelData;

    private void Awake() {
        currentLevel = PlayerData.CurrentLevel;
        currentLevelData = GameData.levels[currentLevel - 1];
    }

    private void Start() {
        EnemyManager.Instance.InitiateLevel(currentLevelData);
    }
}

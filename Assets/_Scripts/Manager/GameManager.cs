using System;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    private int currentLevel;
    private LevelData currentLevelData;

    private int currentPoints;

    // UI
    [SerializeField] private PointsUI pointsUI;

    private void OnEnable() {
        EventDispatcher.Add<EventDefine.OnPointsAdded>(OnPointsAdded);
        EventDispatcher.Add<EventDefine.OnLoseGame>(OnLoseGame);
    }


    private void OnDisable() {
        EventDispatcher.Remove<EventDefine.OnPointsAdded>(OnPointsAdded);
        EventDispatcher.Remove<EventDefine.OnLoseGame>(OnLoseGame);
    }

    private void Awake() {
        currentLevel = PlayerData.CurrentLevel;
        currentLevelData = GameData.levels[currentLevel - 1];
        currentPoints = 0;
    }

    private void Start() {
        EnemyManager.Instance.InitiateLevel(currentLevelData);
    }

    private void OnPointsAdded(IEventParam param) {
        EventDefine.OnPointsAdded _param = (EventDefine.OnPointsAdded)param;

        currentPoints += _param.points;
        pointsUI.SetText(currentPoints.ToString());
    }

    private void OnLoseGame(IEventParam param) {
        Time.timeScale = 0;
        Debug.Log("YOU LOST");
    }

    private void OnEnemyDead(IEventParam param)
    {
        
    }
}

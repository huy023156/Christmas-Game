using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager> {
    private LevelData currentLevelData;
    private List<EnemyBase> enemies;
    private bool isFirstEnemy = true;

    private void OnEnable() {
        EventDispatcher.Add<EventDefine.OnEnemyDead>(OnEnemyDead);
    }

    private void OnDisable() {
        EventDispatcher.Remove<EventDefine.OnEnemyDead>(OnEnemyDead);
    }

    private void Awake() {
        enemies = new List<EnemyBase>();
    }

    public void InitiateLevel(LevelData levelData) {
        currentLevelData = levelData;
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies() {
        for (int i = 0; i < currentLevelData.pointsToWin; i++) {
            EnemyData randomEnemyData = EnemyData.GetRandomEnemyData();
            yield return StartCoroutine(SpawnEnemy(randomEnemyData));
        }
        Debug.Log("All enemies spawned");
    }

    private IEnumerator SpawnEnemy(EnemyData enemyData) {
        if (isFirstEnemy) {
            isFirstEnemy = false;
            yield return null;
        } else {
            yield return new WaitForSeconds(Random.Range(4f, 5f)); 
        }
        GameObject enemyObject = Instantiate(Resources.Load<GameObject>(enemyData.PrefabName), transform);
        EnemyBase enemy = enemyObject.GetComponent<EnemyBase>();
        enemy.SetUp(enemyData);
        enemies.Add(enemy);
    }

    private void OnEnemyDead(IEventParam param) {
        EventDefine.OnEnemyDead _param = (EventDefine.OnEnemyDead)param;
        enemies.Remove(_param.enemy);
        StartCoroutine(CheckWinCondition());
    }

    private IEnumerator CheckWinCondition() {
        yield return null;
        if (GameManager.Instance.CurrentPoints >= currentLevelData.pointsToWin) {
            EventDispatcher.Dispatch(new EventDefine.OnWinGame());
            Debug.Log("All enemies defeated, level ended");
        }
    }

    public void ClearEnemies() {
        StopAllCoroutines();

        foreach (var enemy in enemies) {
            if (enemy != null) {
                Destroy(enemy.gameObject);
            }
        }

        enemies.Clear();
    }
}

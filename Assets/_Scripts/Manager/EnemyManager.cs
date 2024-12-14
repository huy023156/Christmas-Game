using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager> {
    private LevelData currentLevelData;
    private PhaseData currentPhaseData;
    private int currentPhaseIndex;
    private int totalPhaseAmount;
    private int enemyRemainingAmount;

    private List<EnemyBase> enemies;

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
        totalPhaseAmount = levelData.phases.Length;
        StartCoroutine(SpawnEnemyByPhase(levelData.phases[currentPhaseIndex]));
    }

    private IEnumerator SpawnEnemyByPhase(PhaseData phaseData) {
        currentPhaseData = phaseData;
        enemyRemainingAmount = currentPhaseData.enemies.Length;

        
        for (int i = 0; i < phaseData.enemies.Length; i++) {
            yield return StartCoroutine(SpawnEnemy(phaseData.enemies[i]));
        }
    }

    private IEnumerator SpawnEnemy(EnemyData enemyData) {
        yield return new WaitForSeconds(Random.Range(2f, 3f)); 
        GameObject enemyObject = Instantiate(Resources.Load<GameObject>(enemyData.PrefabName), transform);
        EnemyBase enemy = enemyObject.GetComponent<EnemyBase>();
        enemy.SetUp(enemyData);
        enemies.Add(enemy);
    }

    private void CheckWinCondition() {
        currentPhaseIndex++;

        if (currentPhaseIndex >= totalPhaseAmount) {
            EventDispatcher.Dispatch(new EventDefine.OnWinGame());
            Debug.Log("Defeat all phases, level ended");
            return;
        } else {
            StartCoroutine(SpawnEnemyByPhase(currentLevelData.phases[currentPhaseIndex]));
        }
    }

    private void OnEnemyDead(IEventParam param)
    {
        EventDefine.OnEnemyDead _param = (EventDefine.OnEnemyDead)param;
        enemies.Remove(_param.enemy);
        enemyRemainingAmount--;
        if (enemyRemainingAmount <= 0) {
            CheckWinCondition();
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
        enemyRemainingAmount = 0;
    }
}

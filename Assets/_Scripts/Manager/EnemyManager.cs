using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager> {
    private LevelData currentLevelData;
    private PhaseData currentPhaseData;
    private int currentPhaseIndex;
    private int totalPhaseAmount;

    private List<EnemyBase> enemies;

    [SerializeField] private Transform spawnPivotTransform;

    float spawnTimerMax = 2f; // Set the spawn timer interval
    float spawnTimer = 0f;
    private float timer;

    private void Awake() {
        enemies = new List<EnemyBase>();
    }

    public void InitiateLevel(LevelData levelData) {
        currentLevelData = levelData;
        totalPhaseAmount = levelData.phases.Length;
        SpawnEnemyByPhase(levelData.phases[currentPhaseIndex]);
    }

    private void SpawnEnemyByPhase(PhaseData phaseData) {
        currentPhaseData = phaseData;

        // Setup
        Camera camera = Camera.main;
        Vector3 leftEdge = camera.ScreenToWorldPoint(new Vector2(0, 0));
        float spawnRange = Mathf.Abs(leftEdge.x) - 0.3f;
        
        for (int i = 0; i < phaseData.enemies.Length; i++) {
            float spawnX = Random.Range(-spawnRange, spawnRange);
            Vector3 spawnPosition = new Vector3(spawnX, spawnPivotTransform.position.y, spawnPivotTransform.position.z);
            Debug.Log(phaseData.enemies[i].PrefabName);
            GameObject enemyObject = Instantiate(Resources.Load<GameObject>(phaseData.enemies[i].PrefabName), spawnPosition, Quaternion.identity, spawnPivotTransform);
            EnemyBase enemy = enemyObject.GetComponent<EnemyBase>();
            enemy.SetUp(LabelManager.Instance.GetRandomLabelType());
            enemies.Add(enemy);
        }
    }

    private void CheckWinCondition() {
        currentPhaseIndex++;

        if (currentPhaseIndex >= totalPhaseAmount) {
            // TODO: win level
            Debug.Log("Defeat all phases, level ended");
            return;
        } else {
            SpawnEnemyByPhase(currentLevelData.phases[currentPhaseIndex]);
        }
    }

    public bool CheckLabelInEnemies(LabelManager.LabelType labelType) {
        bool found = false;
        
        List<EnemyBase> enemiesToRemove = new List<EnemyBase>();

        foreach (EnemyBase enemy in enemies) {
            if (enemy.LabelType == labelType) {
                enemy.Hit();
                enemiesToRemove.Add(enemy);
                found = true;
            }
        }

        foreach (EnemyBase enemy in enemiesToRemove) {
            enemies.Remove(enemy);
        }

        if (enemies.Count == 0) {
            CheckWinCondition();
        }

        return found;
    } 
}

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager> {
    private LevelData currentLevelData;
    private PhaseData currentPhaseData;
    private int currentPhaseIndex;
    private int totalPhaseAmount;
    private int enemyRemainingAmount;

    private List<EnemyBase> enemies;

    [SerializeField] private Transform spawnPivotTransform;

    float spawnTimerMax = 2f; // Set the spawn timer interval
    float spawnTimer = 0f;
    private float timer;

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
        SpawnEnemyByPhase(levelData.phases[currentPhaseIndex]);
    }

    private async void SpawnEnemyByPhase(PhaseData phaseData) {
        currentPhaseData = phaseData;
        enemyRemainingAmount = currentPhaseData.enemies.Length;

        // Setup
        Camera camera = Camera.main;
        Vector3 leftEdge = camera.ScreenToWorldPoint(new Vector2(0, 0));
        float spawnRange = Mathf.Abs(leftEdge.x) - 0.3f;
        
        for (int i = 0; i < phaseData.enemies.Length; i++) {
            float spawnX = Random.Range(-spawnRange, spawnRange);
            await SpawnEnemy(phaseData.enemies[i], spawnX);
        }
    }

    private async Awaitable SpawnEnemy(EnemyData enemyData, float spawnX) {
        await Awaitable.WaitForSecondsAsync(Random.Range(2f, 3f)); 
        Vector3 spawnPosition = new Vector3(spawnX, spawnPivotTransform.position.y, spawnPivotTransform.position.z);
        GameObject enemyObject = Instantiate(Resources.Load<GameObject>(enemyData.PrefabName), spawnPosition, Quaternion.identity, spawnPivotTransform);
        EnemyBase enemy = enemyObject.GetComponent<EnemyBase>();
        enemy.SetUp(enemyData);
        enemies.Add(enemy);
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

    private void OnEnemyDead(IEventParam param)
    {
        EventDefine.OnEnemyDead _param = (EventDefine.OnEnemyDead)param;
        enemies.Remove(_param.enemy);
        enemyRemainingAmount--;
        Debug.Log("EnemyDead");
        if (enemyRemainingAmount <= 0) {
            CheckWinCondition();
        }

    }

}

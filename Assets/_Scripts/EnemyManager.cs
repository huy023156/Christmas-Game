using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager> {
    private List<EnemyBase> enemies;

    [SerializeField] private Transform spawnPivotTransform;
    [SerializeField] private Transform enemyPrefab;

    float spawnTimerMax = 2f; // Set the spawn timer interval
    float spawnTimer = 0f;
    private float timer;

    private void Awake() {
        enemies = new List<EnemyBase>();
    }

    private void Update() {
        HandleSpawnEnemy();
    }

    private void SpawnEnemy() {
        Camera camera = Camera.main;
        Vector3 leftEdge = camera.ScreenToWorldPoint(new Vector2(0, 0));
        float spawnRange = Mathf.Abs(leftEdge.x) - 0.3f;
        
        float spawnX = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPosition = new Vector3(spawnX, spawnPivotTransform.position.y, spawnPivotTransform.position.z);
        
        EnemyBase enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, spawnPivotTransform).GetComponent<EnemyBase>();
        enemy.SetUp(LabelManager.Instance.GetRandomLabelType());
        enemies.Add(enemy);
    }

    private void HandleSpawnEnemy() {
        if (spawnTimer < 0) {
            SpawnEnemy();
            spawnTimer = spawnTimerMax;
        }

        spawnTimer -= Time.deltaTime;
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

        return found;
    } 
}

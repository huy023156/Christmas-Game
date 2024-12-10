using System.Collections.Generic;
using UnityEngine;

public class EnemyData : BaseData {
    public string id;
    public int labelCount = 1;
    public bool isMultiple = true;
    public float speedMultiplier = 1f;

    public string Name => $"_ENEMY_{GetAssetName(id)}"; 
    public string PrefabName => $"ENEMY_{GetAssetName(id)}";  // ENEMY_SEA_GULL - Load in resources

    public override bool Equals(object obj) {
        if (obj == null || GetType() != obj.GetType())
            return false;

        EnemyData other = (EnemyData)obj;
        return id == other.id;
    }

    public override int GetHashCode() {
        return id.GetHashCode();
    }

    public static EnemyData single = new() { id = "Single"};
    public static EnemyData doublee = new() { id = "Double", labelCount = 2 };
    public static EnemyData triple = new() { id = "Triple", labelCount = 3 };
    public static EnemyData quadruple = new() { id = "Quadruple", labelCount = 4 };
    public static EnemyData quintuple  = new() { id = "Quintuple", labelCount = 5 };
    public static EnemyData bomb = new() { id = "Bomb", labelCount = 3, isMultiple = false };

    public static Dictionary<string, EnemyData> symbolToEnemyDataDict = new()
    {
        { "S", single },
        { "D", doublee },
        { "T", triple },
        { "Q", quadruple },
        { "P", quintuple },
        { "B", bomb }
    };

    // convert "SSDD" to [single, single, doublee, doublee]
    public static EnemyData[] ConvertStringToEnemiesArray(string enemiesString) {
        EnemyData[] enemiesArray = new EnemyData[enemiesString.Length];

        foreach (char enemySymbol in enemiesString) {
            if (!symbolToEnemyDataDict.TryGetValue(enemySymbol.ToString(), out EnemyData enemyData)) {
                Debug.LogError($"Enemy symbol '{enemySymbol}' not found in symbolToEnemyDataDict.");
                continue;
            }

            enemiesArray[enemiesString.IndexOf(enemySymbol)] = enemyData;
        }

        return enemiesArray;
    }
}

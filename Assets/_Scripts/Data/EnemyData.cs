using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class EnemyData : BaseData {
    public string id;
    public int labelCount = 1;
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

    public static List<EnemyData> enemyDataList = new()
    {
        { single },
        { doublee },
        { triple },
        { quadruple },
        { quintuple },
    };

    public static Dictionary<EnemyData, int> enemyDataProbabilityDict = new()
    {
        { single, 400 },
        { doublee, 100 },
        { triple, 0 },
        { quadruple, 0 },
        { quintuple, 0 }
    };

    public static EnemyData GetRandomEnemyData()
    {
        var weightedList = new List<EnemyData>();
        foreach (var kvp in enemyDataProbabilityDict)
        {
            if (kvp.Value <= 0) continue;

            for (int i = 0; i < kvp.Value; i++)
            {
                weightedList.Add(kvp.Key);
            }
        }

        int randomIndex = Random.Range(0, weightedList.Count);
        return weightedList[randomIndex];
    }
}

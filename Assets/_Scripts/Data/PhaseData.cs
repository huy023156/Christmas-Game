using System.Collections.Generic;

public class PhaseData : BaseData {
    public EnemyData[] enemies;

    public static PhaseData[] CreatePhases(string enemies) {
        string[] enemiesStrings = enemies.Replace(" ", "").Split(',');
        List<EnemyData[]> enemiesArraysList = new();

        foreach (string enemy in enemiesStrings) {
            EnemyData[] enemiesArray = EnemyData.ConvertStringToEnemiesArray(enemy);
            enemiesArraysList.Add(enemiesArray);
        }

        List<PhaseData> phases = new();
        foreach (EnemyData[] enemiesArray in enemiesArraysList) {
            phases.Add(new PhaseData { enemies = enemiesArray });
        }
        return phases.ToArray();
    }
}

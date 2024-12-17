public class GameData : BaseData {
    private static LevelData level1 = new LevelData(pointsToWin: 30);
    private static LevelData level2 = new LevelData(pointsToWin: 200);
    private static LevelData level3 = new LevelData(pointsToWin: 500);

    public static LevelData[] levels = new LevelData[] {
        level1,
        level2,
        level3,
    };
}

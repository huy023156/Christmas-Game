public class GameData {
    public static LevelData level1 = new LevelData (PhaseData.CreatePhases("SS, SSS, SSS"));
    public static LevelData level2 = new LevelData (PhaseData.CreatePhases("SS, SSS, DDD"));
    public static LevelData level3 = new LevelData (PhaseData.CreatePhases("SS, SSS, DDD"));

    public static LevelData[] levels = new LevelData[] {
        level1,
        level2,
        level3,
    };
}

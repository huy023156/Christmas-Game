public class GameData : BaseData {
    private static LevelData level1 = new LevelData(PhaseData.CreatePhases("SDTSDT"));
    private static LevelData level2 = new LevelData(PhaseData.CreatePhases("SS, SSS, DDD"));
    private static LevelData level3 = new LevelData(PhaseData.CreatePhases("SS, SSS, DDD"));

    public static LevelData[] levels = new LevelData[] {
        level1,
        level2,
        level3,
    };
}

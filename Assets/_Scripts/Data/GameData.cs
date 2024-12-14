public class GameData : BaseData {
    private static LevelData level1 = new LevelData(PhaseData.CreatePhases("SSSSSDSDSDD"));
    private static LevelData level2 = new LevelData(PhaseData.CreatePhases("SSDDDDTDTTD"));
    private static LevelData level3 = new LevelData(PhaseData.CreatePhases("TTTQQQQTDQ"));

    public static LevelData[] levels = new LevelData[] {
        level1,
        level2,
        level3,
    };
}

public class LevelData : BaseData {
    public float speedMultiplier = 1f; 
    public PhaseData[] phases;

    public LevelData(PhaseData[] phases, float speedMultiplier = 1f) {
        this.phases = phases;
        this.speedMultiplier = speedMultiplier;
    }
}

public class LevelData : BaseData {
    public float speedMultiplier = 1f; 

    public LevelData(float speedMultiplier = 1f, int pointsToWin = 100) {
        this.speedMultiplier = speedMultiplier;
    }
}

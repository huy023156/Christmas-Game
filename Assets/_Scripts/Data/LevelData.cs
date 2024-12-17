public class LevelData : BaseData {
    public float speedMultiplier = 1f; 
    public int pointsToWin;

    public LevelData(float speedMultiplier = 1f, int pointsToWin = 100) {
        this.speedMultiplier = speedMultiplier;
        this.pointsToWin = pointsToWin;
    }
}

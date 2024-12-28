using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneName
{
    MainMenuScene,
    SelectLevelScene,
    GameScene,
}

public class Loader 
{
    public static SceneName targetScene;

    public static void Load(SceneName targetScene)
    {
        SceneManager.LoadScene(targetScene.ToString());
        Time.timeScale = 1;
    }
}

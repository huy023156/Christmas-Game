using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {
    [SerializeField] private Button playBtn;
    [SerializeField] private Button quitBtn;

    private void Awake() {
        playBtn.onClick.AddListener(() => {
            Loader.Load(SceneName.GameScene);
        });
        quitBtn.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}

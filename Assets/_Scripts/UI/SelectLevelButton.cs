using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelButton : MonoBehaviour {
    private int level;

    public void SetUp(int level){
        this.level = level;
        GetComponentInChildren<TextMeshProUGUI>().text = level.ToString();
        GetComponent<Button>().interactable = PlayerData.IsLevelUnlocked(level);

        GetComponent<Button>().onClick.AddListener(() => {
            PlayerData.CurrentLevel = level;
            Loader.Load(SceneName.GameScene);
        });
    }
}

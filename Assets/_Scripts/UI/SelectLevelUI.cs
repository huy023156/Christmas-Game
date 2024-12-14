using UnityEngine;

public class SelectLevelUI : MonoBehaviour {
    [SerializeField] private Transform selectLevelButtonPrefab;

    private void Awake() {
        for (int i = 0; i < GameData.levels.Length; i++) {
            Transform selectLevelButton = Instantiate(selectLevelButtonPrefab, transform);
            SelectLevelButton selectLevelBtn = selectLevelButton.GetComponent<SelectLevelButton>();
            selectLevelBtn.SetUp(i + 1);
        }
    }
}

using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class WinGameUI : MonoBehaviour {
    [SerializeField] private Button replayBtn;
    [SerializeField] private Button mainMenuBtn;
    [SerializeField] private Button nextLevelBtn;
    [SerializeField] private TextMeshProUGUI pointsText;

    private void Awake() {
        EventDispatcher.Add<EventDefine.OnWinGame>(OnWinGame);
    }

    private void OnDestroy() {
        EventDispatcher.Remove<EventDefine.OnWinGame>(OnWinGame);
    }

    private void Start() {
        replayBtn.onClick.AddListener(() => {
            Loader.Load(SceneName.GameScene);
        });

        mainMenuBtn.onClick.AddListener(() => {
            Loader.Load(SceneName.MainMenuScene);
        });

        nextLevelBtn.onClick.AddListener(() => {
            PlayerData.IncreaseLevel();
            Loader.Load(SceneName.GameScene);
        });

        gameObject.SetActive(false);
    }

    private void OnWinGame(IEventParam param)
    {
        gameObject.SetActive(true);
        pointsText.text = "Total points: " + GameManager.Instance.CurrentPoints.ToString();
    }
}

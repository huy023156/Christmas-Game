using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {
    [SerializeField] private Button replayBtn;
    [SerializeField] private Button mainMenuBtn;
    [SerializeField] private TextMeshProUGUI pointsText;
    private void Awake() {
        EventDispatcher.Add<EventDefine.OnLoseGame>(OnLoseGame);
    }

    private void OnDestroy() {
        EventDispatcher.Remove<EventDefine.OnLoseGame>(OnLoseGame);
    }

    private void Start() {
        replayBtn.onClick.AddListener(() => {
            Loader.Load(SceneName.GameScene);
        });

        mainMenuBtn.onClick.AddListener(() => {
            Loader.Load(SceneName.MainMenuScene);
        });

        gameObject.SetActive(false);
    }

    private void OnLoseGame(IEventParam param)
    {
        gameObject.SetActive(true);
        pointsText.text = "Total points: " + GameManager.Instance.CurrentPoints.ToString();
    }
}

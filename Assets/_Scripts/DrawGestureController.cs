using System;
using GestureRecognizer;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawGestureController : MonoBehaviour {

    private DrawDetector drawDetector;
	private bool isDragging;

    private void OnEnable() {
        EventDispatcher.Add<EventDefine.OnEnemyDead>(OnEnemyDead);
    }

    private void OnDisable() {
        EventDispatcher.Remove<EventDefine.OnEnemyDead>(OnEnemyDead);
    }

    private void Awake() {
        drawDetector = GetComponent<DrawDetector>();
    }

    public void OnRecognize(RecognitionResult result) {
        if (result == RecognitionResult.Empty) {
			return;
		}

        Debug.Log("Recognized :" + result.gesture.id);

        LabelManager.LabelType labelTypeFound = LabelManager.Instance.GetLabelTypeByString(result.gesture.id);
        EnemyManager.Instance.CheckLabelInEnemies(labelTypeFound);
	}

    private void OnEnemyDead(IEventParam param)
    {
        drawDetector.ClearLines();
    }
}

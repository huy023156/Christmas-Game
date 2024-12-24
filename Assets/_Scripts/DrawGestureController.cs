using GestureRecognizer;
using UnityEngine;

public class DrawGestureController : MonoBehaviour {
    private DrawDetector drawDetector;

    private void OnEnable() {
        EventDispatcher.Add<EventDefine.OnBalloonPopped>(OnBalloonPopped);
    }

    private void OnDisable() {
        EventDispatcher.Remove<EventDefine.OnBalloonPopped>(OnBalloonPopped);
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
        EventDispatcher.Dispatch(new EventDefine.OnLabelRecognized { labelType = labelTypeFound });
	}

    private void OnBalloonPopped(IEventParam param)
    {
        drawDetector.ClearLines();
    }
}

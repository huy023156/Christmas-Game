using GestureRecognizer;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawGestureController : MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler {

    private DrawDetector drawDetector;

    private float lastClickTime = 0f; // Thời gian nhấn trước đó
	private const float doubleClickThreshold = 0.1f; // Ngưỡng thời gian cho nhấn hai lần

	private bool isDragging;

    private void Awake() {
        drawDetector = GetComponent<DrawDetector>();
    }

    public void OnRecognize(RecognitionResult result) {
        if (result == RecognitionResult.Empty) {
			return;
		}

        Debug.Log("Recognized :" + result.gesture.id);

        LabelManager.LabelType labelTypeFound = LabelManager.Instance.GetLabelTypeByString(result.gesture.id);
        if (EnemyManager.Instance.CheckLabelInEnemies(labelTypeFound)) {
            drawDetector.ClearLines();
        }
	}

    public void OnPointerClick (PointerEventData eventData) {
		if (isDragging) 
			return;

		if (eventData.clickCount == 2 || (eventData.clickCount == 1 && Time.time - lastClickTime < doubleClickThreshold)) {
			drawDetector.ClearLines();
		}

		lastClickTime = Time.time;
	}

    public void OnDrag(PointerEventData eventData) {
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData) {
		isDragging = false;
    }
}

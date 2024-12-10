using UnityEngine;

[ExecuteInEditMode]
public class LineScript : MonoBehaviour {
    [SerializeField] private Transform connectedObjectTransform;

    private LineRenderer lineRenderer;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update() {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, connectedObjectTransform.position);    
    }
}

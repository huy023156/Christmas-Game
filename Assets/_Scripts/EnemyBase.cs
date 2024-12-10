using TMPro;
using UnityEngine;

public class EnemyBase : MonoBehaviour {
    [SerializeField] private float moveSpeed = 5;

    [SerializeField] private Transform ballonTransform;
    [SerializeField] private Transform giftTransform;
    [SerializeField] private TextMeshPro labelText;


    public LabelManager.LabelType LabelType { get ; private set ; }

    public void SetUp(LabelManager.LabelType labelType) {
        LabelType = labelType;
        Debug.Log("Enemy set label type to: " + LabelType);
        labelText.text = labelType.ToString();
    }

    private void Update() {
        transform.position += moveSpeed * Time.deltaTime * Vector3.up;
    }

    public virtual void Hit() {
        ballonTransform.gameObject.SetActive(false);
        giftTransform.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}

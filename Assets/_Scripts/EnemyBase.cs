using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour {
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private Transform giftTransform;
    private EnemyData enemyData;
    private int labelCount;

    private Balloon[] balloonArray;

    private bool isDead = false;
    public bool IsDead => isDead;

    private BoxCollider2D boxCollider2D;
    private Rigidbody2D rb;

    private void OnEnable() {
        EventDispatcher.Add<EventDefine.OnLabelRecognized>(OnLabelRecognized);
    }

    private void OnDisable() {
        EventDispatcher.Remove<EventDefine.OnLabelRecognized>(OnLabelRecognized);
    }


    public void SetUp(EnemyData enemyData) {
        boxCollider2D = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        this.enemyData = enemyData;
        labelCount = enemyData.labelCount;
        isDead = false;

        SetUpPosition();
        SetUpBalloons();
    }


    private void Update() {
        if (isDead) return;

        transform.position += moveSpeed * GameManager.Instance.GlobalSpeedMultiplier * Time.deltaTime * Vector3.down;
    }

    private void SetUpBalloons() {
        balloonArray = GetComponentsInChildren<Balloon>();
        if (balloonArray.Length != labelCount) {
            Debug.LogError($"Balloon count is {balloonArray.Length}, wrong, check prefab!!");
        }
        
        List<LabelManager.LabelType> labelUsedList = new List<LabelManager.LabelType>();
        foreach (Balloon balloon in balloonArray) {
            LabelManager.LabelType labelType = LabelManager.Instance.GetRandomLabelType();
            while (labelUsedList.Contains(labelType)) {
                labelType = LabelManager.Instance.GetRandomLabelType();
            }

            labelUsedList.Add(labelType);
            
            Debug.Log("Balloon set label type to: " + labelType);

            balloon.labelType = labelType;

            switch (labelType) {
                case LabelManager.LabelType.Circle:
                    balloon.labelText.text = "O";
                    break;
                case LabelManager.LabelType.Vertical:
                    balloon.labelText.text = "|";
                    break;
                case LabelManager.LabelType.Horizontal:
                    balloon.labelText.text = "—";
                    break;
                case LabelManager.LabelType.V:
                    balloon.labelText.text = LabelManager.LabelType.V.ToString();
                    break;
                case LabelManager.LabelType.W:
                    balloon.labelText.text = LabelManager.LabelType.W.ToString().ToLower();
                    break;
            }
        }
    }

    private void SetUpPosition() {
        // Handle position
        Camera camera = Camera.main;
        // Lấy cạnh trên của màn hình
        float topEdgeY = camera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        // Thêm khoảng offset để enemy spawn cao hơn màn hình một chút
        float spawnY = topEdgeY + 1f; 
        
        // Tính toán vị trí spawn theo chiều ngang
        Vector3 leftEdge = camera.ScreenToWorldPoint(new Vector2(0, 0));
        float spawnRange = Mathf.Abs(leftEdge.x) * 0.7f;
        float spawnX = Random.Range(-spawnRange, spawnRange);
        
        // Set vị trí spawn
        transform.position = new Vector3(spawnX, spawnY, 0);
    }

    public virtual void Hit() {
    }

    public virtual void Die() {
        moveSpeed = 0;
        isDead = true;
        giftTransform.GetComponent<Rigidbody2D>().gravityScale = 1;
        EventDispatcher.Dispatch(new EventDefine.OnEnemyDead { enemy = this });
        EventDispatcher.Dispatch(new EventDefine.OnPointsAdded { points = enemyData.labelCount} );
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.TryGetComponent(out DeadZone deadZone)){
            if (!isDead) 
                EventDispatcher.Dispatch(new EventDefine.OnLoseGame());
        }
    }

    public virtual void Explode(Vector3 position) {
        Debug.Log("COINT BURSTTTTTTTTT");
        Destroy(gameObject);
    }

    private void OnLabelRecognized(IEventParam param) {
        if (isDead) {
            return;
        }

        EventDefine.OnLabelRecognized _param = (EventDefine.OnLabelRecognized)param;

        List<Balloon> balloonToRemove = new List<Balloon>();

        foreach (Balloon balloon in balloonArray) {
            if (balloon != null && balloon.labelType == _param.labelType) {
                labelCount--;
                Destroy(balloon.gameObject);
                Hit();
                EventDispatcher.Dispatch(new EventDefine.OnBalloonPopped());
                SoundManager.Instance.PlaySound(SoundManager.SoundType.Pop);
                if (labelCount <= 0) {
                    Die();
                }
            }
        }
    }
}

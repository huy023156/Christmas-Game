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

        // Handle balloons
        balloonArray = GetComponentsInChildren<Balloon>();
        if (balloonArray.Length != labelCount) {
            Debug.LogError($"Balloon count is {balloonArray.Length}, wrong, check prefab!!");
        }
        
        foreach (Balloon balloon in balloonArray) {
            LabelManager.LabelType labelType = LabelManager.Instance.GetRandomLabelType();
            Debug.Log("Balloon set label type to: " + labelType);

            balloon.labelType = labelType;
            balloon.labelText.text = labelType.ToString();
        }
    }


    private void Update() {
        if (isDead) return;
        transform.position += moveSpeed * Time.deltaTime * Vector3.down;

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
                if (labelCount <= 0) {
                    Die();
                }
            }
        }

        // foreach (Balloon balloon in balloonToRemove) {
        //     balloonArray.
        //     Destroy(balloon);
        // }
    }
}

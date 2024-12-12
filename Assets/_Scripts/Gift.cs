using UnityEngine;

public class Gift : MonoBehaviour {
    [SerializeField] private EnemyBase enemy;

        private void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.TryGetComponent(out DeadZone deadZone)){
            if (enemy.IsDead) 
                enemy.Explode(transform.position);
        }
    }
}

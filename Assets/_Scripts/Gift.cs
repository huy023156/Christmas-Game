using UnityEngine;

public class Gift : MonoBehaviour {
    [SerializeField] private EnemyBase enemy;
    private bool hasInstantiated = false;

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.TryGetComponent(out DeadZone deadZone)){
            if (enemy.IsDead && !hasInstantiated) { 
                hasInstantiated = true; 
                ScreenShake.Instance.Shake(0.5f, 0.5f);
                SoundManager.Instance.PlaySound(SoundManager.SoundType.Destroy, 6f);
                Instantiate(Resources.Load<Transform>("COIN_BURST"), transform.position + Vector3.up, Quaternion.identity);
                Destroy(gameObject);
                Destroy(enemy.gameObject);
            } 
        }
    }
}
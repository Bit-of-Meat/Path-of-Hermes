using UnityEngine;

public class ProjectileAddon : MonoBehaviour {
    [SerializeField] private int damage;
    private Rigidbody rb;
    private bool targetHit;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (targetHit) return;
        else targetHit = true;

        if(collision.gameObject.GetComponent<BasicEnemyDone>() != null) {
            BasicEnemyDone enemy = collision.gameObject.GetComponent<BasicEnemyDone>();
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

        transform.SetParent(collision.transform);
    }
}
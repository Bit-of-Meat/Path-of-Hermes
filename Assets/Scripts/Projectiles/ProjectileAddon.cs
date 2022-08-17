using UnityEngine;

public class ProjectileAddon : MonoBehaviour {
    [SerializeField] private int damage;
    [SerializeField] private ThrowingTutorial TS;
    [SerializeField] private Rigidbody rb;

    private int _collide = 0;

    private void OnCollisionEnter(Collision collision) {
        _collide++;

        BasicEnemyDone enemy = collision.gameObject.GetComponent<BasicEnemyDone>();

        if(enemy != null) enemy.TakeDamage(damage);
    }

    public void Update() {
        if (_collide == 2) {
            //TS.goback = true;
            _collide = 0;
        }
    }
}
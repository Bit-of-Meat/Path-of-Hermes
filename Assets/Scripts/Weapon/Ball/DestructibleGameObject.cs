using UnityEngine;

namespace Weapons {
    /// <summary>
    /// Logic for destructible object
    /// </summary>
    public class DestructibleGameObject : MonoBehaviour {
        [SerializeField] private int _health;

        public void TakeDamage(int damage) {
            _health -= damage;

            if (_health <= 0) Destroy(gameObject);
        }
    }
}
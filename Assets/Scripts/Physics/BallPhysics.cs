using UnityEngine;
using Player;

namespace Weapons.Ball {
    /// <summary>
    /// Responsible for the physics of the ball
    /// </summary>
    public class BallPhysics : MonoBehaviour {
        [SerializeField] private int _damage;
        [SerializeField] private int _maximumNumberOfBounces = 3;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private PlayerBallThrow _playerBall;
        private int _numberOfCollisions = 0;

        /// <summary>
        /// Handle all ball collisions
        /// </summary>
        /// <param name="collision">Describes a collision</param>
        private void OnCollisionEnter(Collision collision) {
            _numberOfCollisions++;

            DestructibleGameObject _object = collision.gameObject.GetComponent<DestructibleGameObject>();
            if(_object != null) _object.TakeDamage(_damage);

            if (_numberOfCollisions == _maximumNumberOfBounces) {
                ResetNumbersOfCollisions();
                _playerBall.Lerping();
            }
        }

        /// <summary>
        /// Resets the collision counter
        /// </summary>
        public void ResetNumbersOfCollisions() {
            _numberOfCollisions = 0;
        }
    }
}
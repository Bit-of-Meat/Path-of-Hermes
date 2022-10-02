using UnityEngine;

namespace Player {
    /// <summary>
    /// Logic for teleporting player to point when he fall from map
    /// </summary>
    public class PlayerFallFromMap : MonoBehaviour {
        [SerializeField] private Transform _player;
        [SerializeField] private float _respawnHeight = -10f;

        private void Update() {
            if (_player.position.y < _respawnHeight)
                _player.position = transform.position;
        }
    }
}
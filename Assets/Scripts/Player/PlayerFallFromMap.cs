using UnityEngine;

public class PlayerFallFromMap : MonoBehaviour {
    [SerializeField] private Transform _player;
    [SerializeField] private float _respawnHeight = -10f;

    void Update() {
        if (_player.position.y < _respawnHeight)
            _player.position = transform.position;
    }
}

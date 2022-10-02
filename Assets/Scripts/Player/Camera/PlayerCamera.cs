using UnityEngine;

namespace Player.Camera {
    public class PlayerCamera : MonoBehaviour {
        [SerializeField] private PlayerInput _input;
        [SerializeField] private Transform _orientation;
        private Vector2 _sensitivity;
        private Vector2 _rotation;

        private void Start() {
            _sensitivity.x = PlayerPrefs.GetFloat("sensitivityX", 10f);
            _sensitivity.y = PlayerPrefs.GetFloat("sensitivityY", 10f);
        }

        public void Update() {
            Vector2 _mouse = _input.LookDirection * _sensitivity * Time.deltaTime;

            _rotation.y += _mouse.x;

            _rotation.x -= _mouse.y;
            _rotation.x = Mathf.Clamp(_rotation.x, -90f, 90f);

            // Set camera rotation
            transform.rotation = Quaternion.Euler(_rotation.x, _rotation.y, 0);
            _orientation.rotation = Quaternion.Euler(0, _rotation.y, 0);
        }
    }
}
using UnityEngine;

namespace Player.Camera {
    // Done
    public class MoveCamera : MonoBehaviour {
        [SerializeField] private Transform cameraPosition;

        private void Update() {
            transform.position = cameraPosition.position;
        }
    }
}
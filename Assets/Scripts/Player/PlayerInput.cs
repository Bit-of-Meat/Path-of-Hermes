using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
    /// <summary>
    /// Handles player input (mouse & keyboard, gamepad, joystick, etc.)
    /// </summary>
    public class PlayerInput : MonoBehaviour {
        /// <summary>
        /// Returns true if jump key is pressed
        /// </summary>
        /// <value>Boolean</value>
        public bool IsJump {
            get => _isJump;
        }
        /// <summary>
        /// Returns true if jump key is pressed
        /// </summary>
        /// <value>Boolean</value>
        public bool IsCrouch {
            get => _isCrouch;
        }
        /// <summary>
        /// Returns true if sprint key is pressed
        /// </summary>
        /// <value>Boolean</value>
        public bool IsSprint {
            get => _isSprint;
        }
        /// <summary>
        /// Returns true if fire key is pressed
        /// </summary>
        /// <value>Boolean</value>
        public bool IsFire {
            get => _isFire;
        }
        /// <summary>
        /// Returns WASD, Gamepad left stick, etc.
        /// </summary>
        /// <value>Vector from [-1,-1] to [1,1]</value>
        public Vector2 MovementDirection {
            get => _movementDirection;
        }
        /// <summary>
        /// Returns Mouse delta, Gamepad right stick, etc.
        /// </summary>
        /// <value>Vector from [-1,-1] to [1,1]</value>
        public Vector2 LookDirection {
            get => _lookDirection;
        }

        [SerializeField] private InGameMenu _inGameMenu;
        private PlayerControls _controls;
        private bool _isJump;
        private bool _isCrouch;
        private bool _isSprint;
        private bool _isFire;
        private Vector2 _movementDirection;
        private Vector2 _lookDirection;

        private void Awake() {
            _controls = new PlayerControls();
            _controls.UI.InGameMenu.started += _inGameMenu.Show;
            SetCursorLock(true);
        }

        private void OnEnable() => _controls.Enable();
        private void OnDisable() => _controls.Disable();

        private void Update() {
            _isJump = _controls.Player.Jump.IsPressed();
            _isCrouch = _controls.Player.Crouch.IsPressed();
            _isSprint = _controls.Player.Sprint.IsPressed();
            _isFire = _controls.Player.Fire.IsPressed();

            _movementDirection = _controls.Player.Movement.ReadValue<Vector2>();
            _lookDirection = _controls.Player.Look.ReadValue<Vector2>();
        }

        /// <summary>
        /// Sets cursor invisible and lock it to center of display
        /// </summary>
        /// <param name="isLock">true is lock</param>
        public static void SetCursorLock(bool isLock) {
            Cursor.lockState = isLock ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !isLock;
        }
    }
}
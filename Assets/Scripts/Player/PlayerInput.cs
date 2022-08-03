using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles player input (mouse & keyboard, gamepad, joystick, etc.)
/// </summary>
public class PlayerInput : MonoBehaviour {
    /// <summary>
    /// Returns true if jump key is pressed
    /// </summary>
    public bool IsJump {
        get => _isJump;
    }
    /// <summary>
    /// Returns true if jump key is pressed
    /// </summary>
    public bool IsCrouch {
        get => _isCrouch;
    }
    /// <summary>
    /// Returns true if sprint key is pressed
    /// </summary>
    public bool IsSprint {
        get => _isSprint;
    }
    /// <summary>
    /// Returns WASD, Gamepad left stick, etc.
    /// </summary>
    public Vector2 MovementDirection {
        get => _movementDirection;
    }
    
    /// <summary>
    /// Returns Mouse delta, Gamepad right stick, etc.
    /// </summary>
    public Vector2 LookDirection {
        get => _lookDirection;
    }

    [SerializeField] private InGameMenu _inGameMenu;

    private PlayerControls _controls;
    private bool _isJump;
    private bool _isCrouch;
    private bool _isSprint;
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

        _movementDirection = _controls.Player.Movement.ReadValue<Vector2>();
        _lookDirection = _controls.Player.Look.ReadValue<Vector2>();
    }

    public static void SetCursorLock(bool isLock) {
        Cursor.lockState = isLock ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isLock;
    }
}

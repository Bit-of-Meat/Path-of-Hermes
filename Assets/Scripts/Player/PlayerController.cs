using System.Collections;
using UnityEngine;
using FSM;
using TMPro;

public class PlayerController : MonoBehaviour {
    public PlayerInput Input { get => _input; }
    public Rigidbody RigidBody { get => _rigidbody; }
    //Movement
    public float DesiredMoveSpeed { get; set; }
    public float WalkSpeed { get => _walkSpeed; }
    public float SprintSpeed { get => _sprintSpeed; }
    // Ground
    public bool IsGrounded { get => _isGrounded; }
    public LayerMask GroundLayerMask { get => _groundLayerMask; }
    public float GroundDrag { get => _groundDrag; }
    public bool IsAbove { get => _isAbove; }
    public float PlayerHeight { get => _playerHeight; }
    public bool IsLeftWall { get => _isLeftWall; }
    public bool IsRightWall { get => _isRightWall; }
    // Jumping
    public float JumpHeight { get => _jumpHeight; }
    public float JumpCooldown { get => _jumpCooldown; }
    public bool ReadyToJump { get; set; }
    // Crouching
    public bool Crouching { get; set; }
    public float CrouchSpeed { get => _crouchSpeed; }
    public float StartYScale { get => _startYScale; }
    public float CrouchYScale { get => _crouchYScale; }
    public bool ExitingSlope { get; set; }
    // Wallrun
    public float WallrunDrag { get => _wallrunDrag; }
    public float StartWallrunForce { get => _startWallrunForce; }
    public float WallrunTick { get => _wallrunTick; }
    
    private StateMachine<PlayerStates> _stateMachine;
    private bool _isGrounded;
    private bool _isAbove;
    private bool _isLeftWall;
    private bool _isRightWall;

    // Movement
    private float _moveSpeed;
    private float _lastDesiredMoveSpeed;
    private Vector3 _moveDirection;
    // Crouching
    private float _startYScale;
    // Sloping
    private RaycastHit _slopeHit;
    // For smooth movement
    private Coroutine smooth;

    [SerializeField] private PlayerInput _input;
    [SerializeField] private Rigidbody _rigidbody;

    [Header("Movement")]
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _sprintSpeed = 7f;
    [SerializeField] private float _speedIncreaseMultiplier = 1.5f;
    [SerializeField] private float _slopeIncreaseMultiplier = 2.5f;
    [SerializeField] private float _groundDrag = 8f;

    [Header("Jumping")]
    [SerializeField] private float _jumpHeight = 3f;
    [SerializeField] private float _jumpCooldown = 0.25f;
    [SerializeField] private float airMultiplier = 0.4f;

    [Header("Crouching")]
    [SerializeField] private float _crouchSpeed = 3.5f;
    [SerializeField] private float _crouchYScale = 0.5f;
    
    [Header("Ground Check")]
    [SerializeField] private float _playerHeight = 2f;
    [SerializeField] private LayerMask _groundLayerMask;

    [Header("Slope Handling")]
    [SerializeField] private float _maxSlopeAngle = 40f;
    [SerializeField] private Transform _orientation;

    [Header("Easy Wallrun v0.1")]
    [SerializeField] private float _wallrunDrag;
    [SerializeField] private float _startWallrunForce;
    [SerializeField] private float _wallrunTick;

    [SerializeField] private TextMeshProUGUI speed;

    private void Start() {
        RigidBody.freezeRotation = true;
        ReadyToJump = true;
        _startYScale = transform.localScale.y;

        _stateMachine = new StateMachine<PlayerStates>();
        
        _stateMachine.AddState(PlayerStates.Ground, new PlayerGroundState(this))
                     .AddState(PlayerStates.Jump, new PlayerJumpState(this))
                     .AddState(PlayerStates.Fall, new PlayerFallState(this))
                     .AddState(PlayerStates.Wallrun, new PlayerWallrunState(this))

                     .AddTransition(PlayerStates.Ground, PlayerStates.Fall, (_) => !IsGrounded)
                     .AddTransition(PlayerStates.Ground, PlayerStates.Jump, (_) => _input.IsJump && ReadyToJump)

                     .AddTransition(PlayerStates.Jump, PlayerStates.Fall, (_) => ReadyToJump)
                     
                     .AddTransition(PlayerStates.Fall, PlayerStates.Ground, (_) => IsGrounded)
                     .AddTransition(PlayerStates.Fall, PlayerStates.Wallrun, (_) => IsLeftWall || IsRightWall)

                     .AddTransition(PlayerStates.Wallrun, PlayerStates.Jump, (_) => _input.IsJump && ReadyToJump)
                     .AddTransition(PlayerStates.Wallrun, PlayerStates.Fall, (_) => !(IsLeftWall || IsRightWall))
                     .AddTransition(PlayerStates.Wallrun, PlayerStates.Ground, (_) => IsGrounded);

        _stateMachine.SetStartState(PlayerStates.Ground);
        _stateMachine.Init();
    }

    public void Update() {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight * 0.5f + 0.1f, GroundLayerMask);
        _isAbove = Physics.Raycast(transform.position, Vector3.up, PlayerHeight * 0.5f + 0.1f, GroundLayerMask);
        _isLeftWall = Physics.Raycast(transform.position, -_orientation.right, PlayerHeight * 0.5f + 0.1f, GroundLayerMask);
        _isRightWall = Physics.Raycast(transform.position, _orientation.right, PlayerHeight * 0.5f + 0.1f, GroundLayerMask);

        _stateMachine.OnLogic();
        SpeedControl();
        Smooth();
        DisplaySpeed();
    }
    
    private void FixedUpdate() {
        MovePlayer();
    }

    private void Smooth() {
        // check if desired move speed has changed drastically
        if (Mathf.Abs(DesiredMoveSpeed - _lastDesiredMoveSpeed) > 4f && _moveSpeed != 0) {
            if (smooth != null) StopCoroutine(smooth);
            smooth = StartCoroutine(SmoothlyLerpMoveSpeed());
        } else {
            _moveSpeed = DesiredMoveSpeed;
        }

        _lastDesiredMoveSpeed = DesiredMoveSpeed;
    }

    private IEnumerator SmoothlyLerpMoveSpeed() {
        // smoothly lerp movementSpeed to desired value
        float time = 0;
        float difference = Mathf.Abs(DesiredMoveSpeed - _moveSpeed);
        float startValue = _moveSpeed;
        
        while (time < difference) {
            _moveSpeed = Mathf.Lerp(startValue, DesiredMoveSpeed, time / difference);

            if (OnSlope()) {
                float slopeAngle = Vector3.Angle(Vector3.up, _slopeHit.normal);
                float slopeAngleIncrease = 1 + (slopeAngle / 90f);

                time += Time.deltaTime * _speedIncreaseMultiplier * _slopeIncreaseMultiplier * slopeAngleIncrease;
            } else
                time += Time.deltaTime * _speedIncreaseMultiplier;

            yield return null;
        }

        _moveSpeed = DesiredMoveSpeed;
    }

    private void SpeedControl() {
        // limiting speed on slope
        if (OnSlope() && !ExitingSlope) {
            if (RigidBody.velocity.magnitude > _moveSpeed)
                RigidBody.velocity = RigidBody.velocity.normalized * _moveSpeed;
        } else {
            // limiting speed on ground or in air
            Vector3 flatVel = new Vector3(RigidBody.velocity.x, 0f, RigidBody.velocity.z);

            // limit velocity if needed
            if (flatVel.magnitude > _moveSpeed) {
                Vector3 limitedVel = flatVel.normalized * _moveSpeed;
                RigidBody.velocity = new Vector3(limitedVel.x, RigidBody.velocity.y, limitedVel.z);
            }
        }
    }

    private void MovePlayer() {
        _moveDirection = _orientation.forward * _input.MovementDirection.y + _orientation.right * _input.MovementDirection.x;

        if (OnSlope() && !ExitingSlope) {
            RigidBody.AddForce(GetSlopeMoveDirection(_moveDirection) * _moveSpeed * 20f, ForceMode.Force);

            if (RigidBody.velocity.y > 0)
                RigidBody.AddForce(Vector3.down * 80f, ForceMode.Force);
        } else if (IsGrounded) {
            RigidBody.AddForce(_moveDirection.normalized * _moveSpeed * 10f, ForceMode.Force);
        } else if (!IsGrounded) {
            RigidBody.AddForce(_moveDirection.normalized * _moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        RigidBody.useGravity = !OnSlope();
    }

    public bool OnSlope() {
        if (Physics.Raycast(transform.position, Vector3.down, out _slopeHit, _playerHeight * 0.5f + 0.3f)) {
            float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            return angle < _maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction) {
        return Vector3.ProjectOnPlane(direction, _slopeHit.normal).normalized;
    }

    public void DisplaySpeed() {
        Vector3 flatVel = new Vector3(RigidBody.velocity.x, 0f, RigidBody.velocity.z);
        speed.text = "Speed: " + Mathf.Round(flatVel.magnitude);
    }  
}
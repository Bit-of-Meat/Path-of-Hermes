using System.Collections;
using UnityEngine;
using FSM;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //Base
    public bool inWindZone = false;
    public GameObject windZone;
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

    public float AirMultiplier { get => _airMultiplier; }

    public Transform Orientation { get => _orientation; }

    public float Speed { get => new Vector3(RigidBody.velocity.x, 0f, RigidBody.velocity.z).magnitude; }

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
    [SerializeField] private float _airMultiplier = 0.4f;

    [Header("Crouching")]
    [SerializeField] private float _crouchSpeed = 3.5f;
    [SerializeField] private float _crouchYScale = 0.5f;
    
    [Header("Ground Check")]
    [SerializeField] private float _playerHeight = 2f;
    [SerializeField] private LayerMask _groundLayerMask;

    [Header("Easy Wallrun v0.1")]
    [SerializeField] private float _wallrunDrag;
    [SerializeField] private float _startWallrunForce;
    [SerializeField] private float _wallrunTick;
    
    [SerializeField] private Transform _orientation;
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
                     .AddState(PlayerStates.Walljump, new PlayerWalljumpState(this))

                     .AddTransition(PlayerStates.Ground, PlayerStates.Fall, (_) => !IsGrounded)
                     .AddTransition(PlayerStates.Ground, PlayerStates.Jump, (_) => _input.IsJump && ReadyToJump)

                     .AddTransition(PlayerStates.Jump, PlayerStates.Fall, (_) => ReadyToJump)
                     .AddTransition(PlayerStates.Walljump, PlayerStates.Fall, (_) => ReadyToJump)
                     
                     .AddTransition(PlayerStates.Fall, PlayerStates.Ground, (_) => IsGrounded)
                     .AddTransition(PlayerStates.Fall, PlayerStates.Wallrun, (_) => IsLeftWall || IsRightWall)

                     .AddTransition(PlayerStates.Wallrun, PlayerStates.Walljump, (_) => _input.IsJump && ReadyToJump)
                     .AddTransition(PlayerStates.Wallrun, PlayerStates.Fall, (_) => !(IsLeftWall || IsRightWall))
                     .AddTransition(PlayerStates.Wallrun, PlayerStates.Ground, (_) => IsGrounded);

        _stateMachine.SetStartState(PlayerStates.Ground);
        _stateMachine.Init();
    }

    public void Update() {
        _isGrounded = Physics.BoxCast(transform.position, new Vector3(0.5f, 0.05f, 0.5f), Vector3.down, Quaternion.identity, PlayerHeight * 0.5f, GroundLayerMask);
        _isAbove = Physics.Raycast(transform.position, Vector3.up, PlayerHeight * 0.5f + 0.05f, GroundLayerMask);

        _isLeftWall = Physics.Raycast(transform.position, -_orientation.right, PlayerHeight * 0.5f + 0.05f, GroundLayerMask);
        _isRightWall = Physics.Raycast(transform.position, _orientation.right, PlayerHeight * 0.5f + 0.05f, GroundLayerMask);

        _stateMachine.OnLogic();
        DisplaySpeed();
    }

    // Nikita Arkhipov
    private void FixedUpdate() {
        if(inWindZone) {
            _rigidbody.AddForce(windZone.GetComponent<Wind>().direction * windZone.GetComponent<Wind>().strength);
        }
    }

    void OnTriggerEnter(Collider coll) {
       if(coll.gameObject.tag == "windArea") {
            windZone = coll.gameObject;
            inWindZone = true;
        }
    }

    void OnTriggerExit(Collider coll) {
        if (coll.gameObject.tag == "windArea") {
            inWindZone = false;
        }
    }
    
    void OnDrawGizmos() {
        Gizmos.color = Color.black;
        Gizmos.DrawCube(transform.position + Vector3.down * (PlayerHeight * 0.5f), new Vector3(0.5f, 0.05f, 0.5f));
    }

    public void DisplaySpeed() {
        speed.text = "Speed: " + Mathf.Round(Speed) + " | " + _stateMachine.ActiveStateName.ToString();
    }  
}
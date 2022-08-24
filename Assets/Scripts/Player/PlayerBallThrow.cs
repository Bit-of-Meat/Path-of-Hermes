using UnityEngine;
using FSM;
using TMPro;

/// <summary>
/// Responsible for throwing the ball and returning it
/// </summary>
public class PlayerBallThrow : MonoBehaviour {
    public Transform PlayerCamera { get => _playerCamera; }
    public Transform PlayerHand { get => _playerHand; }

    public Rigidbody BallRigidbody { get => _ballRigidbody; }
    public GameObject BallObject { get => _ballObject; }
    public BallPhysics BallPhysics { get => _ballPhysics; }

    public float LerpTime { get => _lerpTime; }
    public float BackDistance { get => _backDistance; }
    public float ThrowForce { get => _throwForce; }
    public float GravityMultiplier { get => _gravityMultiplier; }
    
    /// <summary>
    /// Can the player throw the ball
    /// </summary>
    public bool CanThrow { get; set; } = true;

    [SerializeField] private PlayerInput _input;

    [Header("Positions")]
    [SerializeField] private Transform _playerCamera;
    [SerializeField] private Transform _playerHand;

    [Header("Ball")]
    [SerializeField] private Rigidbody _ballRigidbody;
    [SerializeField] private GameObject _ballObject;
    [SerializeField] private BallPhysics _ballPhysics;
    
    [Header("Throw settings")]
    [SerializeField] private float _lerpTime = 4f;
    [SerializeField] private float _backDistance = 12f;
    [SerializeField] private float _throwForce = 16f;
    [SerializeField] private float _gravityMultiplier = 1f;
    
    [SerializeField] private TextMeshProUGUI text;

    /// <summary>
    /// True if the ball is at a distance greater than back distance
    /// </summary>
    private bool _isBackDistance = false;
    /// <summary>
    /// True if the ball is close to the hand
    /// </summary>
    private bool _isHandDistance = false;

    private StateMachine<BallStates> _stateMachine;

    private void Start() {
        _stateMachine = new StateMachine<BallStates>();
        
        _stateMachine.AddState(BallStates.Pocket, new BallPocketState(this))
                     .AddState(BallStates.Throw, new BallThrowState(this))
                     .AddState(BallStates.Flight, new BallFlightState(this))
                     .AddState(BallStates.Lerp, new BallLerpState(this))

                     .AddTransition(BallStates.Pocket, BallStates.Throw, (_) => _input.IsFire && CanThrow)
                     .AddTransition(BallStates.Throw, BallStates.Flight)
                     .AddTransition(BallStates.Flight, BallStates.Lerp, (_) => _isBackDistance)
                     .AddTransition(BallStates.Lerp, BallStates.Pocket, (_) => _isHandDistance);

        _stateMachine.SetStartState(BallStates.Pocket);
        _stateMachine.Init();
    }

    /// <summary>
    /// Handle physics
    /// </summary>
    private void FixedUpdate() {
        _isBackDistance = Vector3.Distance(_ballRigidbody.position, _playerHand.position) > _backDistance;
        _isHandDistance = Vector3.Distance(_ballRigidbody.position, _playerHand.position) <= 0.5f;

        text.text = _stateMachine.ActiveStateName.ToString();

        _stateMachine.OnLogic();
    }

    public void Lerping() {
        _stateMachine.RequestStateChange(BallStates.Lerp, true);
    }
}
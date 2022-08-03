using System.Collections;
using UnityEngine;
using FSM;

class PlayerGroundState : StateBase<PlayerStates> {
    private PlayerController _controller;
    private StateMachine<PlayerStates> _stateMachine;

    public PlayerGroundState(PlayerController controller) : base(needsExitTime: false) {
        _controller = controller;
        _stateMachine = new StateMachine<PlayerStates>();
    }

    public override void Init() {
        _stateMachine.AddState(PlayerStates.Idle, new PlayerIdleState(_controller))
                     .AddState(PlayerStates.Walk, new PlayerWalkState(_controller))
                     .AddState(PlayerStates.Run, new PlayerSprintState(_controller))
                     .AddState(PlayerStates.Crouch, new PlayerCrouchState(_controller))
                     .AddState(PlayerStates.Slide, new PlayerSlideState(_controller))

                     .AddTransition(PlayerStates.Idle, PlayerStates.Walk, (_) => _controller.Input.MovementDirection != Vector2.zero)
                     .AddTransition(PlayerStates.Idle, PlayerStates.Crouch, (_) => _controller.Input.IsCrouch)
                     
                     .AddTransition(PlayerStates.Walk, PlayerStates.Idle, (_) => _controller.Input.MovementDirection == Vector2.zero)
                     .AddTransition(PlayerStates.Walk, PlayerStates.Run, (_) => _controller.Input.IsSprint)
                     .AddTransition(PlayerStates.Walk, PlayerStates.Crouch, (_) => _controller.Input.IsCrouch)

                     .AddTransition(PlayerStates.Run, PlayerStates.Walk, (_) => !_controller.Input.IsSprint)
                     .AddTransition(PlayerStates.Run, PlayerStates.Crouch, (_) => _controller.Input.IsCrouch)
                     
                     .AddTransition(PlayerStates.Crouch, PlayerStates.Idle, (_) => !_controller.Input.IsCrouch && !_controller.IsAbove);

        _stateMachine.SetStartState(PlayerStates.Idle);
        _stateMachine.Init();
    }

    public override void OnEnter() {
        _controller.RigidBody.drag = _controller.GroundDrag;
        _stateMachine.OnEnter();
    }

    public override void OnLogic() {
        _stateMachine.OnLogic();
    }
}
using FSM;

class PlayerFallState : StateBase<PlayerStates> {
    private PlayerController _controller;

    public PlayerFallState(PlayerController controller) : base(needsExitTime: false) {
        _controller = controller;
    }

    public override void OnEnter() {
        _controller.RigidBody.drag = 0f;
    }
}
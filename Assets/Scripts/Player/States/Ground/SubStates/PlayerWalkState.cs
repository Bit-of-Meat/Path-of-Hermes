using FSM;

class PlayerWalkState : StateBase<PlayerStates> {
    private PlayerController _controller;

    public PlayerWalkState(PlayerController controller) : base(needsExitTime: false) {
        _controller = controller;
    }

    public override void OnEnter() {
        _controller.DesiredMoveSpeed = _controller.WalkSpeed;
    }
}
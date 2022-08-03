using FSM;

class PlayerSlideState : StateBase<PlayerStates> {
    private PlayerController _controller;

    public PlayerSlideState(PlayerController controller) : base(needsExitTime: false) {
        _controller = controller;
    }

    public override void OnEnter() {
    }

    public override void OnLogic() {

    }
}
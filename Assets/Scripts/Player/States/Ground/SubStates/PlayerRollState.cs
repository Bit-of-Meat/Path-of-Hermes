using FSM;

class PlayerRollState : StateBase<PlayerStates> {
    private PlayerController _controller;

    public PlayerRollState(PlayerController controller) : base(needsExitTime: false) {
        _controller = controller;
    }

    public override void OnEnter() {
    }

    public override void OnLogic() {
    }
}
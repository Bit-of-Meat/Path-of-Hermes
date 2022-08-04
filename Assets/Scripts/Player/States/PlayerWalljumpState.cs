using UnityEngine;
using FSM;

public class PlayerWalljumpState : StateBase<PlayerStates> {
    private PlayerController _controller;
    private float _upforce;
    public PlayerWalljumpState(PlayerController controller) : base(needsExitTime: false) {
        _controller = controller;
    }

    public override void OnEnter() {
    }

    public override void OnLogic() {

    }
}

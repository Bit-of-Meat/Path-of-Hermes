using UnityEngine;
using FSM;

public class BallFlightState : StateBase<BallStates> {
    private PlayerBallThrow _ballThrow;
    public BallFlightState(PlayerBallThrow ballThrow) : base(needsExitTime: false) {
        _ballThrow = ballThrow;
    }

    public override void OnLogic() {
        _ballThrow.BallRigidbody.AddForce(_ballThrow.GravityMultiplier * (Physics.gravity * _ballThrow.BallRigidbody.mass), ForceMode.Force);
    }
}

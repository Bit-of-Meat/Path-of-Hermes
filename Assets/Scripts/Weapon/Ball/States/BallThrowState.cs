using UnityEngine;
using FSM;

public class BallThrowState : StateBase<BallStates> {
    private PlayerBallThrow _ballThrow;
    public BallThrowState(PlayerBallThrow ballThrow) : base(needsExitTime: false) {
        _ballThrow = ballThrow;
    }

    public override void OnEnter() {
        _ballThrow.BallObject.SetActive(true);
        _ballThrow.CanThrow = false;
        
        _ballThrow.BallRigidbody.velocity = Vector3.zero;
        _ballThrow.BallRigidbody.isKinematic = false;
        _ballThrow.BallRigidbody.transform.position = _ballThrow.PlayerCamera.position;

        _ballThrow.BallRigidbody.AddForce(_ballThrow.PlayerCamera.forward * _ballThrow.ThrowForce, ForceMode.Impulse);
    }
}

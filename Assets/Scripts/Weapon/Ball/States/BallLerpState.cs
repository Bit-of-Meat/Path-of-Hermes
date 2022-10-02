using UnityEngine;
using FSM;
using Player;

namespace Weapons.Ball.States {
    public class BallLerpState : StateBase<BallStates> {
        private PlayerBallThrow _ballThrow;
        public BallLerpState(PlayerBallThrow ballThrow) : base(needsExitTime: false) {
            _ballThrow = ballThrow;
        }

        public override void OnEnter() {
            _ballThrow.BallRigidbody.isKinematic = true;
            _ballThrow.BallRigidbody.velocity = Vector3.zero;
        }

        public override void OnLogic() {
            Vector3 _movePosition = Vector3.Slerp(_ballThrow.BallRigidbody.position, _ballThrow.PlayerHand.position, _ballThrow.LerpTime * Time.fixedDeltaTime);
            _ballThrow.BallRigidbody.MovePosition(_movePosition);
        }
    }
}
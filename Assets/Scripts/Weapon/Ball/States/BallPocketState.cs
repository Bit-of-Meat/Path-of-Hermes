using FSM;
using Player;

namespace Weapons.Ball.States {
    public class BallPocketState : StateBase<BallStates> {
        private PlayerBallThrow _ballThrow;
        public BallPocketState(PlayerBallThrow ballThrow) : base(needsExitTime: false) {
            _ballThrow = ballThrow;
        }

        public override void OnEnter() {
            _ballThrow.BallObject.SetActive(false);
            _ballThrow.CanThrow = true;

            _ballThrow.BallPhysics.ResetNumbersOfCollisions();
        }
    }
}
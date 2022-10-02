using FSM;
using Player.States;

namespace Player.GroundSubstates {
    public class PlayerIdleState : StateBase<PlayerStates> {
        private PlayerController _controller;

        public PlayerIdleState(PlayerController controller) : base(needsExitTime: false) {
            _controller = controller;
        }
    }
}
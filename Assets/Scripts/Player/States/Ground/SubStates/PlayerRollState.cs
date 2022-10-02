using FSM;
using Player.States;

namespace Player.GroundSubstates {
    public class PlayerRollState : StateBase<PlayerStates> {
        private PlayerController _controller;

        public PlayerRollState(PlayerController controller) : base(needsExitTime: false) {
            _controller = controller;
        }
    }
}
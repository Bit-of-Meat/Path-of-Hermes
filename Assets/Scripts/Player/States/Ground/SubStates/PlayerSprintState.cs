using FSM;
using Player.States;

namespace Player.GroundSubstates {
    public class PlayerSprintState : StateBase<PlayerStates> {
        private PlayerController _controller;

        public PlayerSprintState(PlayerController controller) : base(needsExitTime: false) {
            _controller = controller;
        }

        public override void OnEnter() {
            _controller.DesiredMoveSpeed = _controller.SprintSpeed;
        }
    }
}
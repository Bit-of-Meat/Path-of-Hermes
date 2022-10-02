using UnityEngine;
using FSM;

namespace Player.States {
    public class PlayerFallState : StateBase<PlayerStates> {
        private PlayerController _controller;

        public PlayerFallState(PlayerController controller) : base(needsExitTime: false) {
            _controller = controller;
        }

        public override void OnEnter() {
            _controller.RigidBody.drag = 0f;
        }

        public override void OnLogic() {
            Vector3 _moveDirection = _controller.Orientation.forward * _controller.Input.MovementDirection.y + _controller.Orientation.right * _controller.Input.MovementDirection.x;
            _controller.RigidBody.AddForce(_moveDirection.normalized * _controller.DesiredMoveSpeed * 10f * _controller.AirMultiplier, ForceMode.Force);
        }
    }
}
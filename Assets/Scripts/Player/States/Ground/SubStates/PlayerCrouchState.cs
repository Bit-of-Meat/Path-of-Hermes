using UnityEngine;
using FSM;
using Player.States;

namespace Player.GroundSubstates {
    public class PlayerCrouchState : StateBase<PlayerStates> {
        private PlayerController _controller;

        public PlayerCrouchState(PlayerController controller) : base(needsExitTime: false) {
            _controller = controller;
        }

        public override void OnEnter() {
            _controller.transform.localScale = new Vector3(_controller.transform.localScale.x, _controller.CrouchYScale, _controller.transform.localScale.z);
            _controller.RigidBody.AddForce(Vector3.down * 5f, ForceMode.Impulse);

            _controller.Crouching = true;

            _controller.DesiredMoveSpeed = _controller.CrouchSpeed;
        }

        public override void OnExit() {
            _controller.transform.localScale = new Vector3(_controller.transform.localScale.x, _controller.StartYScale, _controller.transform.localScale.z);

            _controller.Crouching = false;
        }
    }
}
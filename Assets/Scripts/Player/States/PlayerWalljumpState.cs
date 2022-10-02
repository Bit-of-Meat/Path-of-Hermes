using System.Collections;
using UnityEngine;
using FSM;

namespace Player.States {
    public class PlayerWalljumpState : StateBase<PlayerStates> {
        private PlayerController _controller;
        private float _upforce;
        public PlayerWalljumpState(PlayerController controller) : base(needsExitTime: false) {
            _controller = controller;
        }

        public override void OnEnter() {
            _controller.RigidBody.drag = 0f;
            _controller.ReadyToJump = false;

            Jump();
            
            _controller.StartCoroutine(ResetJump(_controller.JumpCooldown));
        }

        private void Jump() {
            Vector3 _direction = _controller.Orientation.forward;

            _controller.RigidBody.velocity = new Vector3(_controller.RigidBody.velocity.x, 0f, _controller.RigidBody.velocity.z);
            _controller.RigidBody.AddForce(_direction * _controller.WallrunJumpForce, ForceMode.Impulse);
        }

        private IEnumerator ResetJump(float cooldown) {
            yield return new WaitForSeconds(cooldown);

            _controller.ReadyToJump = true;
        }
    }
}
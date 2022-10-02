using System.Collections;
using UnityEngine;
using FSM;

namespace Player.States {
    public class PlayerJumpState : StateBase<PlayerStates> {
        private PlayerController _controller;

        public PlayerJumpState(PlayerController controller) : base(needsExitTime: false) {
            _controller = controller;
        }

        public override void OnEnter() {
            _controller.RigidBody.drag = 0f;
            _controller.ReadyToJump = false;

            Jump();
            
            _controller.StartCoroutine(ResetJump(_controller.JumpCooldown));
        }
        
        public override void OnLogic() {
            Vector3 _moveDirection = _controller.Orientation.forward * _controller.Input.MovementDirection.y + _controller.Orientation.right * _controller.Input.MovementDirection.x;
            _controller.RigidBody.AddForce(_moveDirection.normalized * _controller.DesiredMoveSpeed * 10f * _controller.AirMultiplier, ForceMode.Force);
        }
        
        private void Jump() {
            _controller.RigidBody.velocity = new Vector3(_controller.RigidBody.velocity.x, 0f, _controller.RigidBody.velocity.z);
            _controller.RigidBody.AddForce(_controller.transform.up * Mathf.Sqrt(-2f * Physics.gravity.y * _controller.JumpHeight), ForceMode.Impulse);
        }

        private IEnumerator ResetJump(float cooldown) {
            yield return new WaitForSeconds(cooldown);

            _controller.ReadyToJump = true;
        }
    }
}
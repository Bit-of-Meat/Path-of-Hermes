using System.Collections;
using UnityEngine;
using FSM;

class PlayerJumpState : StateBase<PlayerStates> {
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

    private void Jump() {
        _controller.RigidBody.velocity = new Vector3(_controller.RigidBody.velocity.x, 0f, _controller.RigidBody.velocity.z);
        _controller.RigidBody.AddForce(_controller.transform.up * Mathf.Sqrt(-2f * Physics.gravity.y * _controller.JumpHeight), ForceMode.Impulse);
    }

    private IEnumerator ResetJump(float cooldown) {
        yield return new WaitForSeconds(cooldown);

        _controller.ReadyToJump = true;
    }
}
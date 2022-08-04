using System.Collections;
using UnityEngine;
using FSM;

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
        Vector3 _direction = _controller.IsRightWall ? -_controller.Orientation.right : _controller.Orientation.right;

        _controller.RigidBody.velocity = new Vector3(_controller.RigidBody.velocity.x, 0f, _controller.RigidBody.velocity.z);
        _controller.RigidBody.AddForce(_direction * Mathf.Sqrt(-2f * Physics.gravity.y * _controller.JumpHeight), ForceMode.Impulse);
    }

    private IEnumerator ResetJump(float cooldown) {
        yield return new WaitForSeconds(cooldown);

        _controller.ReadyToJump = true;
    }
}

using UnityEngine;
using FSM;

class PlayerFallState : StateBase<PlayerStates> {
    private PlayerController _controller;

    public PlayerFallState(PlayerController controller) : base(needsExitTime: false) {
        _controller = controller;
    }

    public override void OnEnter() {
        _controller.RigidBody.drag = 0f;
    }

    public override void OnLogic() {
        MovePlayer();
        SpeedControl();
    }
    
    private void MovePlayer() {
        Vector3 _moveDirection = _controller.Orientation.forward * _controller.Input.MovementDirection.y + _controller.Orientation.right * _controller.Input.MovementDirection.x;
        _controller.RigidBody.AddForce(_moveDirection.normalized * _controller.DesiredMoveSpeed * 10f * _controller.AirMultiplier, ForceMode.Force);
    }
    
    private void SpeedControl() {
        Vector3 flatVel = new Vector3(_controller.RigidBody.velocity.x, 0f, _controller.RigidBody.velocity.z);

        if (flatVel.magnitude > _controller.DesiredMoveSpeed) {
            Vector3 limitedVel = flatVel.normalized * _controller.DesiredMoveSpeed;
            _controller.RigidBody.velocity = new Vector3(limitedVel.x, _controller.RigidBody.velocity.y, limitedVel.z);
        }
    }
}
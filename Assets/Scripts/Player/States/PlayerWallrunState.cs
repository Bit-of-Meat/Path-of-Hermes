using UnityEngine;
using FSM;

namespace Player.States {
    public class PlayerWallrunState : StateBase<PlayerStates> {
        private PlayerController _controller;
        private float _upforce;
        
        public PlayerWallrunState(PlayerController controller) : base(needsExitTime: false) {
            _controller = controller;
        }

        public override void OnEnter() {
            _controller.RigidBody.drag = _controller.WallrunDrag;
            _upforce = _controller.StartWallrunForce;
        }

        public override void OnLogic() {
            _controller.RigidBody.velocity = new Vector3(_controller.RigidBody.velocity.x, _upforce, _controller.RigidBody.velocity.z);
            _upforce -= _controller.WallrunTick * Time.deltaTime;
        }
    }
}
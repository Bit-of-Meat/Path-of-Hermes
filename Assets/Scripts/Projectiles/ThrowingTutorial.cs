using UnityEngine;

public class ThrowingTutorial : MonoBehaviour {
    [Header("Objects")]
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _hand;

    [Header("Projectile")]
    [SerializeField] private Rigidbody _projectile;
    [SerializeField] private MeshRenderer _projectileMesh;
    [SerializeField] private Collider _projectileCollider;
    
    [Header("Throw settings")]
    [SerializeField] private float _lerpTime = 1f;
    [SerializeField] private float _backDistance = 4f;
    [SerializeField] private float _throwForce = 10f;
    [SerializeField] private float _throwUpwardForce = 12f;
    [SerializeField] private PlayerInput _input;
    
    private bool _isLerping = false;
    private bool _readyToThrow = true;

    void FixedUpdate() {
        if (_isLerping) {
            Vector3 _movePosition = Vector3.Slerp(_projectile.position, _hand.position, _lerpTime * Time.fixedDeltaTime);
            
            _projectile.MovePosition(_movePosition);

            _isLerping = !(Vector3.Distance(_projectile.position, _hand.position) <= 0.5f);
            
            if (!_isLerping) {
                Show(false);
                _projectile.transform.position = _hand.position;
            }
        } else {
            _isLerping = Vector3.Distance(_projectile.position, _hand.position) > _backDistance;
            if (_isLerping) _projectile.isKinematic = true;
        }
        
        if (_input.IsFire && _readyToThrow)
            Throw();
    }

    private void Show(bool isActive) {
        _projectileMesh.enabled = isActive;
        _projectileCollider.enabled = isActive;
        _projectile.isKinematic = !isActive;
        _readyToThrow = !isActive;
    }

    public void Throw() {
        Show(true);

        Vector3 _forceDirection = _camera.forward;
        
        RaycastHit _hit;
        if (Physics.Raycast(_camera.position, _camera.forward, out _hit, 500f)) {
            _forceDirection = (_hit.point - _hand.position).normalized;
        }

        // add force
        Vector3 _forceToAdd = _forceDirection * _throwForce + _hand.up * _throwUpwardForce;
        _projectile.AddForce(_forceToAdd, ForceMode.Impulse);
    }
}
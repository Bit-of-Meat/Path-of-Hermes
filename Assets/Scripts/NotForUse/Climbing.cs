using UnityEngine;

public class Climbing : MonoBehaviour {
    [Header("Climb")]
    [SerializeField] private float _wallAngleMax;
    [SerializeField] private float _groundAngleMax;
    [SerializeField] private LayerMask _layerMaskClimb;

    [Header("Heights")]
    [SerializeField] private float _overpassHeight;
    [SerializeField] private float _hangHeight;
    [SerializeField] private float _climbUpHeight;
    [SerializeField] private float _vaultHeight;
    [SerializeField] private float _stepHeight;
    
    [Header("Offsets")]
    [SerializeField] private Vector3 _climbOriginDown;
    [SerializeField] private Vector3 _endOffset;

    [Header("Other")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private CapsuleCollider _capsule;

    private RaycastHit _downRaycastHit;
    
    private bool _climbing;
    private Vector3 _endPosition;
    private Vector3 _forwardDirectionXZ;
    private Vector3 _forwardNormalXZ;

    private bool CanClimb(out RaycastHit downRaycastHit, out RaycastHit forwardRaycastHit, out Vector3 endPosition) {
        endPosition = Vector3.zero;
        downRaycastHit = new RaycastHit();
        forwardRaycastHit = new RaycastHit();
        
        bool _downHit;
        bool _forwardHit;
        bool _overpassHit;
        float _climbHeight;
        float _groundAngle;
        float _wallAngle;

        RaycastHit _downRaycastHit;
        RaycastHit _forwardRaycastHit;
        RaycastHit _overpassRaycastHit;

        Vector3 _downDirection = Vector3.down;
        Vector3 _downOrigin = transform.TransformPoint(_climbOriginDown);

        _downHit = Physics.Raycast(_downOrigin, _downDirection, out _downRaycastHit, _climbOriginDown.y - _stepHeight, _layerMaskClimb);
        if (_downHit) {
            //Forward + overpass cast
            float _forwardDistance = _climbOriginDown.z;
            Vector3 _forwardOrigin = new Vector3(transform.position.x, _downRaycastHit.point.y - 0.1f, transform.position.z);
            Vector3 _overpassOrigin = new Vector3(transform.position.x, _overpassHeight, transform.position.z);

            _forwardDirectionXZ = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
            _forwardHit = Physics.Raycast(_forwardOrigin, _forwardDirectionXZ, out _forwardRaycastHit, _forwardDistance,_layerMaskClimb);
            _overpassHit = Physics.Raycast(_overpassOrigin, _forwardDirectionXZ, out _overpassRaycastHit, _forwardDistance, _layerMaskClimb);
            _climbHeight = _downRaycastHit.point.y - transform.position.y;

            if (_forwardHit)
                if (_overpassHit || _climbHeight < _overpassHeight) {
                    //Angles
                    _forwardNormalXZ = Vector3.ProjectOnPlane(_forwardRaycastHit.normal, Vector3.up);
                    _groundAngle = Vector3.Angle(_downRaycastHit.normal, Vector3.up);
                    _wallAngle = Vector3.Angle(-_forwardNormalXZ, _forwardDirectionXZ);

                    if (_wallAngle <= _wallAngleMax)
                        if (_groundAngle <= _groundAngleMax) {
                            //End offset
                            Vector3 _vectSurface = Vector3.ProjectOnPlane(_forwardDirectionXZ, _downRaycastHit.normal);
                            _endPosition = _downRaycastHit.point + Quaternion.LookRotation(_vectSurface, Vector3.up) * _endOffset;

                            //De-penetration
                            Collider _colliderB = _downRaycastHit.collider;
                            bool _penetrationOverlap = Physics.ComputePenetration(
                                colliderA: _capsule,
                                positionA: _endPosition,
                                rotationA: transform.rotation,
                                colliderB: _colliderB,
                                positionB: _colliderB.transform.position,
                                rotationB: _colliderB.transform.rotation,
                                direction: out Vector3 _penetrationDirection,
                                distance: out float _penetrationDistance);
                            if (_penetrationOverlap)
                                _endPosition += _penetrationDirection * _penetrationDistance;

                            //Up Sweep
                            float _inflate = -0.05f;
                            float _upsweepDistance = _downRaycastHit.point.y - transform.position.y;
                            Vector3 _upSweepDirection = transform.up;
                            Vector3 _upSweepOrigin = transform.position;
                            bool _upSweepHit = CharacterSweep(
                                position: _upSweepOrigin,
                                rotation: transform.rotation,
                                direction: _upSweepDirection,
                                distance: _upsweepDistance,
                                layerMask: _layerMaskClimb,
                                inflate: _inflate);

                            //Forward Sweep
                            Vector3 _forwardSweepOrigin = transform.position + _upSweepDirection * _upsweepDistance;
                            Vector3 _forwardSweepVector = _endPosition - _forwardSweepOrigin;
                            bool _forwardSweepHit = CharacterSweep(
                                position: _forwardSweepOrigin,
                                rotation: transform.rotation,
                                direction: _forwardSweepVector.normalized,
                                distance: _forwardSweepVector.magnitude,
                                layerMask: _layerMaskClimb,
                                inflate: _inflate);
                            
                            if (!_upSweepHit && !_forwardSweepHit) {
                                endPosition = _endPosition;
                                downRaycastHit = _downRaycastHit;
                                forwardRaycastHit = _forwardRaycastHit;
                                return true;
                            }
                        }
                }
        }
        return false;
    }

    private bool CharacterSweep(Vector3 position, Quaternion rotation, Vector3 direction, float distance, LayerMask layerMask, float inflate) {
        //Assuming capusle is on y axis
        float _heightScale = Mathf.Abs(transform.lossyScale.y);
        float _radiusScale = Mathf.Max(Mathf.Abs(transform.lossyScale.x), Mathf.Abs(transform.lossyScale.z));

        float _radius = _capsule.radius * _radiusScale;
        float _totalHeight = Mathf.Max(_capsule.height * _heightScale, _radius * 2);

        Vector3 _capsuleUp = rotation * Vector3.up;
        Vector3 _center = position + rotation * _capsule.center;
        Vector3 _top = _center + _capsuleUp * (_totalHeight / 2 - _radius);
        Vector3 _bottom = _center - _capsuleUp * (_totalHeight / 2 - _radius);

        bool _sweepHit = Physics.CapsuleCast(
            point1: _bottom,
            point2: _top,
            radius: _radius,
            direction: direction,
            maxDistance: distance,
            layerMask: layerMask);

        return _sweepHit;
    }

    private void InitiateClimb() {
        _climbing = true;
        //_newSpeed = 0;
        //_animator
        _capsule.enabled = false;
        _rigidbody.isKinematic = true;

        float _climbHeight = _downRaycastHit.point.y - transform.position.y;
    }
}

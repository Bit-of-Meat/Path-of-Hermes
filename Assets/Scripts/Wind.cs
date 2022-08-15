using UnityEngine;

public class Wind : MonoBehaviour {
    [SerializeField] private float strength;
    [SerializeField] private Vector3 direction;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private LayerMask _windLayerMask;

    [SerializeField] private bool _inWindZone = false;

    private void FixedUpdate() { 
        if (_inWindZone) _rigidbody.AddForce(direction * strength);
    }

    private bool CheckLayer(LayerMask layerMask, int layer) {
        return layerMask == (layerMask | (1 << layer));
    }

    private void OnTriggerEnter(Collider collider) {
        if (CheckLayer(_windLayerMask, collider.gameObject.layer))
            _inWindZone = true;
    }

    private void OnTriggerExit(Collider collider) {
        if (CheckLayer(_windLayerMask, collider.gameObject.layer))
            _inWindZone = false;
    }
}

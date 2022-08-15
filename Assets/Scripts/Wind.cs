using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour {
    [SerializeField] private float strength;
    [SerializeField] private Vector3 direction;
    [SerializeField] private Vector3 _size;
    [SerializeField] private LayerMask _windLayerMask;

    private void FixedUpdate() { 
        var _colliders = Physics.OverlapBox(transform.position, _size, Quaternion.identity, _windLayerMask);
        foreach (var _collider in _colliders)
            if (_collider.attachedRigidbody != null) _collider.attachedRigidbody.AddForce(direction * strength);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, _size);
    }

    private bool CheckLayer(LayerMask layerMask, int layer) {
        return layerMask == (layerMask | (1 << layer));
    }
}

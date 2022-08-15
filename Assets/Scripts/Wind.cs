using System.Collections;
using System.Collections.Generic;
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

    void OnTriggerEnter(Collider coll) {
        if (_windLayerMask == 1 << coll.gameObject.layer)
            _inWindZone = true;
    }

    void OnTriggerExit(Collider coll) {
        if (_windLayerMask == 1 << coll.gameObject.layer)
            _inWindZone = false;
    }
}

using UnityEngine;

/// <summary>
/// Wind physics class based on physics overlap.
/// </summary>
/// <see href="https://github.com/Bit-of-Meat/Path-of-Hermes/blob/master/.github/guide/wind.md">Guide</see>
[HelpURL("https://github.com/Bit-of-Meat/Path-of-Hermes/blob/master/.github/guide/wind.md")]
public class Wind : MonoBehaviour {
    [Header("Settings")]
    [SerializeField] private float _strength = 20f;
    [SerializeField] private LayerMask _windLayerMask;

    [Header("Perfomance")]
    [SerializeField] private int _maximumOfDetectionObjects = 10;

    private Collider[] _buffer;

    private void Start() {
        _buffer = new Collider[_maximumOfDetectionObjects];
    }

    private void FixedUpdate() {
        // non alloc APIs will write collider references to the `buffer` variable - which has been pre allocated.
        int _bufferSize = Physics.OverlapBoxNonAlloc(transform.position, transform.localScale / 2, _buffer, transform.rotation, _windLayerMask, QueryTriggerInteraction.Ignore);

        for (int i = 0; i < _bufferSize; i++) {
            Rigidbody _rigidbody = _buffer[i].attachedRigidbody;
            if (_rigidbody) _rigidbody.AddForce(transform.up * _strength);
        }
    }

    /// <summary>
    /// Draw wind area and direction in editor
    /// </summary>
    private void OnDrawGizmosSelected() {
        Gizmos.matrix = transform.localToWorldMatrix;

        // Draw wind direction arrow
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Vector3.up / 2, Vector3.left / 6);
        Gizmos.DrawLine(Vector3.up / 2, Vector3.right / 6);

        // Draw wind area
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }

    /// <summary>
    /// Checks if the layer mask contains the layer
    /// </summary>
    /// <param name="layerMask">layer mask</param>
    /// <param name="layer">layer</param>
    /// <returns>Does the layer exist in the layer mask</returns>
    private bool CheckLayer(LayerMask layerMask, int layer) {
        return layerMask == (layerMask | (1 << layer));
    }
}
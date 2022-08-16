using UnityEngine;

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
        int _bufferSize = Physics.OverlapBoxNonAlloc(transform.position, transform.localScale / 2, _buffer, transform.rotation, _windLayerMask);
        
        for (int i = 0; i < _bufferSize; i++) {
            Rigidbody _rigidbody = _buffer[i].attachedRigidbody;
            if (_rigidbody) _rigidbody.AddForce(transform.up * _strength);
        }
    }
    
    private void OnDrawGizmosSelected() {
        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.color = Color.red;
        DrawArrow.ForGizmo(Vector3.zero, Vector3.up / 2);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }

    private bool CheckLayer(LayerMask layerMask, int layer) {
        return layerMask == (layerMask | (1 << layer));
    }

    public static class DrawArrow
    {
        public static void ForGizmo(Vector3 pos, Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
        {
            Gizmos.DrawRay(pos, direction);

            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
            Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
        }

        public static void ForGizmo(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
        {
            Gizmos.color = color;
            Gizmos.DrawRay(pos, direction);

            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
            Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
        }

        public static void ForDebug(Vector3 pos, Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
        {
            Debug.DrawRay(pos, direction);

            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Debug.DrawRay(pos + direction, right * arrowHeadLength);
            Debug.DrawRay(pos + direction, left * arrowHeadLength);
        }
        public static void ForDebug(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
        {
            Debug.DrawRay(pos, direction, color);

            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Debug.DrawRay(pos + direction, right * arrowHeadLength, color);
            Debug.DrawRay(pos + direction, left * arrowHeadLength, color);
        }
    }
}
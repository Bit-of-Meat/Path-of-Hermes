using UnityEngine;

public class ThrowingTutorial : MonoBehaviour {
    [Header("References")]
    [SerializeField] private Transform cam;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject objectToThrow;

    [Header("Settings")]
    [SerializeField] private int totalThrows;
    [SerializeField] private float throwCooldown;

    [Header("Throwing")]
    [SerializeField] private KeyCode throwKey = KeyCode.Mouse0;
    [SerializeField] private float throwForce;
    [SerializeField] private float throwUpwardForce;
    [Space]
    [SerializeField] private PlayerInput _input;
    private bool readyToThrow;

    private void Start() {
        readyToThrow = true;
    }

    private void Update() {
        if(_input.IsFire && readyToThrow && totalThrows > 0){
            Throw();
        }
    }

    private void Throw() {
        readyToThrow = false;

        // instantiate object to throw
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate direction
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;
        if(Physics.Raycast(cam.position, cam.forward, out hit, 500f)) {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;
        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);
        totalThrows--;

        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow() {
        readyToThrow = true;
    }
}
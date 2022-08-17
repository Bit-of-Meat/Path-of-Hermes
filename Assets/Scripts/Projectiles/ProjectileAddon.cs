using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAddon : MonoBehaviour
{
    public int damage;
    ThrowingTutorial TS;
    private Rigidbody rb;
    private bool targetHit;

    public int collide;

    private void Start()
    {
        collide = 0;
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        collide++;
        Debug.Log(collide);

        // make sure only to stick to the first target you hit
        if (targetHit)
            return;
        else
            targetHit = true;
        // check if you hit an enemy
        if(collision.gameObject.GetComponent<BasicEnemyDone>() != null)
        {
            BasicEnemyDone enemy = collision.gameObject.GetComponent<BasicEnemyDone>();

            enemy.TakeDamage(damage);

            // destroy projectile
            Destroy(gameObject);
        }

        // make sure projectile moves with target
        transform.SetParent(collision.transform);
    }

    public void Update()
    {
        if (collide == 2)
        {
            TS.goback = true;
            collide = 0;
        }
    }
}
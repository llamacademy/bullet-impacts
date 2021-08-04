using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : PoolableObject
{
    public float AutoDestroyTime = 5f;
    public float MoveSpeed = 2f;
    public Rigidbody Rigidbody;

    private const string DISABLE_METHOD_NAME = "Disable";

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        CancelInvoke(DISABLE_METHOD_NAME);
        Invoke(DISABLE_METHOD_NAME, AutoDestroyTime);
    }

    public void Spawn(Vector3 Forward)
    {
        Rigidbody.AddForce(Forward * MoveSpeed, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.GetContact(0);

        if (collision.gameObject.TryGetComponent<Renderer>(out Renderer renderer))
        {
            BulletImpactManager.Instance.SpawnBulletImpact(contact.point, contact.normal, renderer.sharedMaterial);
        }
        else
        {
            BulletImpactManager.Instance.SpawnBulletImpact(contact.point, contact.normal, null);
        }

        Disable();
    }

    private void Disable()
    {
        Rigidbody.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}

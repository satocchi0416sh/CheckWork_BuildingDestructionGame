using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Player") return;
        Explode();
    }
    
    private void Explode()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 2f);
        Destroy(gameObject);
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Rigidbody rb))
            {
                rb.AddExplosionForce(10f, transform.position, 5f, 1f, ForceMode.Impulse);
            }
        }
    }
}

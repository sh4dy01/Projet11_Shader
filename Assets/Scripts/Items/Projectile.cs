using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed = 10f;
    
    private GameObject _target;
    private Material _trailMaterial;
    private int _ownerLayer;
    private int _damage;

    private void Start()
    {
        _rigidbody.AddForce(transform.forward * _speed, ForceMode.Impulse);
    }
    
    public void SetTarget(int ownerLayer, int damage = 1)
    {
        _damage = damage;
        _ownerLayer = ownerLayer;
    }

    private void OnTriggerEnter(Collider other)
    {
        int otherLayer = other.gameObject.layer;
        if (otherLayer == _ownerLayer) return;
        
        if (other.TryGetComponent(out DamageableEnemy enemy))
        {
            enemy.TakeDamage(_damage);
        }
        
        Destroy(gameObject);
    }
}

using System;
using UnityEngine;

public class DamageableEntity : OutlineObject
{
    [SerializeField] protected int _damage = 1;
    [SerializeField] private int _maxHealth = 5;
    
    private int _currentHealth;
    
    public int Health => _currentHealth;
    public int MaxHealth => _maxHealth;
    public int Damage => _damage;

    public event Action OnHit;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth -= damage, 0, _maxHealth);
        OnHit?.Invoke();
        
        Debug.Log(gameObject.name + " took " + damage + " damage. Current health: " + _currentHealth);
        
        GetHitEffect();
        
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void GetHitEffect()
    {
        // Visual effect
    }
    
    protected virtual void Die()
    {
        DieEffect();
        Destroy(gameObject);
    }
    
    protected virtual void DieEffect()
    {
        // Visual effect
        // Sound effect
    }
}

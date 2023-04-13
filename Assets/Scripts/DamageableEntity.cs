using System;
using System.Collections;
using UnityEngine;

public class DamageableEntity : OutlineObject
{
    [SerializeField] protected int _damage = 1;
    [SerializeField] protected int _maxHealth = 5;
    [Header("Visual effects")]
    [SerializeField] private DissolveController _dissolveController;
    [SerializeField] private HitEffectController _hitEffectController;
    
    protected int _currentHealth;
    private bool _isDead;
    
    public int Health => _currentHealth;
    public int MaxHealth => _maxHealth;
    public int Damage => _damage;
    protected bool IsDead => _isDead;

    public event Action OnHealthUpdate;
    public event Action OnDeath;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    protected void Heal(int amount)
    {
        _currentHealth += amount;
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }

        OnHealthUpdate?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        if (_currentHealth <= 0) return;
        
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);
        OnHealthUpdate?.Invoke();
        
        Debug.Log(gameObject.name + " took " + damage + " damage. Current health: " + _currentHealth);
        
        HitEffect();
        
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        DieEffect(RemoveObject);
        OnDeath?.Invoke();
        _isDead = true;
    }

    protected virtual void HitEffect()
    {
        if (_hitEffectController)
        {
            _hitEffectController.StartEffect();
        }
    }
    
    protected virtual void DieEffect(Action callback)
    {
        if (_dissolveController)
        {
            _dissolveController.Dissolve(callback);
        }
    }
    
    private void RemoveObject()
    {
        Destroy(gameObject);
    }
}

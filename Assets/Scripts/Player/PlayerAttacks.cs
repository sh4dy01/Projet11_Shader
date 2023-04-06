using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAttacks : DamageableEntity
{
    [SerializeField] GameObject _shield;
    [SerializeField] Projectile _projectileToSpawn;
    [SerializeField] Transform _projectileSpawnLocation;
    
    [Header("Attack Settings")]
    [SerializeField] float _attackRange = 1f;
    [SerializeField] float _attackCooldown = 1f;
    
    private bool _isAttacking;
    private bool _isInCooldown;
    private DamageableEnemy _target;
    private NavMeshAgent _navMeshAgent;
    
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    
    private void Update()
    {
        if (!_isAttacking || !_target) return;

        if (IsInRange() && !_isInCooldown)
        {
            AttackTarget();
        }
    }
    
    private bool IsInRange()
    {
        return Vector3.Distance(transform.position, _target.transform.position) <= _attackRange;
    }

    public void SetAttackTarget(DamageableEnemy enemy)
    {
        _isAttacking = true;
        _target = enemy;
        _navMeshAgent.SetDestination(_target.transform.position - _attackRange * Vector3.forward);
    }

    private void AttackTarget()
    {
        GameObject proj = Instantiate(_projectileToSpawn.gameObject, _projectileSpawnLocation.position, Quaternion.identity);
        proj.transform.LookAt(_target.transform);
        proj.GetComponent<Projectile>().SetTarget(_target.gameObject, gameObject.layer, _damage);
        
        StartCoroutine(StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
        _isInCooldown = true;
        yield return new WaitForSeconds(_attackCooldown);
        _isInCooldown = false;
    }
    
    public void ResetAttack()
    {
        _isAttacking = false;
        _target = null;
    }

    protected override void GetHitEffect()
    {
        // player Visual effect
    }
    
    protected override void Die()
    {
        // Respawn
    }
}

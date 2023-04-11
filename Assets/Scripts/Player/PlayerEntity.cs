using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerEntity : DamageableEntity
{
    private Animator _animator;
    //[SerializeField] GameObject _shield;
    [SerializeField] Projectile _projectileToSpawn;
    [SerializeField] Transform _projectileSpawnLocation;
    
    [Header("Attack Settings")]
    [SerializeField] float _distanceAttackRange = 1f;
    [SerializeField] float _meleeAttackRange = 1f;
    [SerializeField] float _attackCooldown = 1f;
    [SerializeField] float _attackRangeOffset = 0.1f;

    private float _currentAttackRange;
    private float _stoppingDistance;
    private int _attackTriggerAnimation;
    private bool _isAttacking;
    private bool _isInCooldown;
    private bool _isRangeAttack;
    private DamageableEnemy _target;
    private CollectibleItem _item;
    private NavMeshAgent _navMeshAgent;
    
    private static readonly int MeleeAttackTrigger = Animator.StringToHash("MeleeAttack");
    private static readonly int RangeAttackTrigger = Animator.StringToHash("RangeAttack");
    protected override void Awake()
    {
        base.Awake();
        
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _stoppingDistance = _navMeshAgent.stoppingDistance;
        
        _animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (!_isAttacking || !_target) return;

        if (!_isInCooldown && IsInRange())
        {
            AttackTarget();
        }
    }
    
    public bool IsInRange()
    {
        return Vector3.Distance(transform.position, _target.transform.position) <= _currentAttackRange + _stoppingDistance + _attackRangeOffset;
    }
    
    public void MeleeAttack(DamageableEnemy enemy)
    {
        _currentAttackRange = _meleeAttackRange + _stoppingDistance;
        _attackTriggerAnimation = MeleeAttackTrigger;
        _isRangeAttack = false;
        SetAttackTarget(enemy);
    }

    public void RangeAttack(DamageableEnemy enemy)
    {
        _currentAttackRange = _distanceAttackRange + _stoppingDistance;
        _attackTriggerAnimation = RangeAttackTrigger;
        _isRangeAttack = true;
        SetAttackTarget(enemy);
    }

    private void SetAttackTarget(DamageableEnemy enemy)
    {
        _isAttacking = true;
        _target = enemy;
        transform.LookAt(_target.transform);

        if (!IsInRange())
        {
            _navMeshAgent.SetDestination(_target.transform.position - _meleeAttackRange * transform.forward);
        } 
        else _navMeshAgent.ResetPath();
    }

    private void AttackTarget()
    {
        _animator.SetTrigger(_attackTriggerAnimation);

        if (_isRangeAttack)
        {
            ThrowProjectile();
        }
        else _target.TakeDamage(_damage);
        
        StartCoroutine(StartCooldown());
    }

    private void ThrowProjectile()
    {
        GameObject proj = Instantiate(_projectileToSpawn.gameObject, _projectileSpawnLocation.position,
            Quaternion.identity);
        proj.transform.LookAt(_target.transform);
        proj.GetComponent<Projectile>().SetTarget(_target.gameObject, gameObject.layer, _damage);
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

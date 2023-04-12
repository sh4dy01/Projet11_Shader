using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerEntity : DamageableEntity
{
    private SkinnedMeshRenderer[] _meshRenderers;
    
    private Animator _animator;
    //[SerializeField] GameObject _shield;
    [SerializeField] Projectile _projectileToSpawn;
    [SerializeField] Transform _projectileSpawnLocation;
    [SerializeField] private GameObject _axeGameObject;
    
    [Header("Attack Settings")]
    [SerializeField] float _distanceAttackRange = 1f;
    [SerializeField] float _meleeAttackRange = 1f;
    [SerializeField] float _attackCooldown = 1f;
    [SerializeField] float _attackRangeCooldown = 3f;
    [SerializeField] float _attackRangeOffset = 0.1f;

    private float _currentAttackRange;
    private float _currentAttackCooldown;
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
    private static readonly int Reset = Animator.StringToHash("Reset");

    protected override void Awake()
    {
        _meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        base.Awake();
        
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _stoppingDistance = _navMeshAgent.stoppingDistance;
        _animator = GetComponent<Animator>();
    }

    protected override void GetMaterials()
    {
        foreach (var mesh in _meshRenderers)
        {
            _materials.AddRange(mesh.materials);
        }
    }

    protected override void RemoveOutlineMaterial()
    {
        foreach (var mesh in _meshRenderers)
        {
            mesh.materials = mesh.materials.Take(mesh.materials.Length - 1).ToArray();
        }
    }
    
    protected override void AddOutlineMaterial()
    {
        foreach (var mesh in _meshRenderers)
        {
            mesh.materials = mesh.materials.Append(GameManager.Instance.PlayerOutlineMaterial).ToArray();
        }
    }

    private void Update()
    {
        if (!_isAttacking || !_target) return;

        if (!_isInCooldown && IsInRange())
        {
            AttackTarget();
        }
    }

    private bool IsInRange()
    {
        return Vector3.Distance(transform.position, _target.transform.position) <= _currentAttackRange + _attackRangeOffset;
    }
    
    public void MeleeAttack(DamageableEnemy enemy)
    {
        _currentAttackRange = _meleeAttackRange + _stoppingDistance;
        _attackTriggerAnimation = MeleeAttackTrigger;
        _currentAttackCooldown = _attackCooldown;
        _isRangeAttack = false;
        _axeGameObject.SetActive(true);
        SetAttackTarget(enemy);
    }

    public void RangeAttack(DamageableEnemy enemy)
    {
        _currentAttackRange = _distanceAttackRange + _stoppingDistance;
        _attackTriggerAnimation = RangeAttackTrigger;
        _currentAttackCooldown = _attackRangeCooldown;
        _isRangeAttack = true;
        _axeGameObject.SetActive(false);
        SetAttackTarget(enemy);
    }

    private void SetAttackTarget(DamageableEnemy enemy)
    {
        _isAttacking = true;
        _target = enemy;
        transform.LookAt(_target.transform);
        _target.OnDeath += ResetAttack;
        _target.OnDeath += ResetAttackAnimation;

        if (!IsInRange())
        {
            _navMeshAgent.SetDestination(_target.transform.position - _currentAttackRange * transform.forward);
        } 
        else _navMeshAgent.ResetPath();
    }

    private void AttackTarget()
    {
        _animator.SetTrigger(_attackTriggerAnimation);
        StartCoroutine(StartCooldown());
    }

    public void TriggerMeleeAttack()
    {
        if (_target)
        {
            _target.TakeDamage(_damage);
        }
    }

    public void ThrowArrow()
    {
        GameObject proj = Instantiate(_projectileToSpawn.gameObject, _projectileSpawnLocation.position,
            Quaternion.identity);
        proj.transform.LookAt(_target.transform);
        proj.GetComponent<Projectile>().SetTarget(gameObject.layer, _damage);
    }

    private IEnumerator StartCooldown()
    {
        _isInCooldown = true;
        yield return new WaitForSeconds(_currentAttackCooldown);
        _isInCooldown = false;
    }
    
    public void ResetAttack()
    {
        _isAttacking = false;
        _target = null;
    }
    
    private void ResetAttackAnimation()
    {
        _animator.SetTrigger(Reset);
    }

    protected override void Die()
    {
        //DieEffect();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

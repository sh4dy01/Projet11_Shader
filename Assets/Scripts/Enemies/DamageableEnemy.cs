using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DamageableEnemy : DamageableEntity
{
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private float _playerRange = 5f;
    
    private NavMeshAgent _agent;
    private Transform _player;

    protected override void Awake()
    {
        base.Awake();
        
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _speed;
        
        CurrentOutlineMaterial = GameManager.Instance.EnemyOutlineMaterial;
    }
    
    private void Update()
    {
        if (!IsDead && IsInRange())
        {
            _agent.SetDestination(_player.position);
        }
    }

    private bool IsInRange()
    {
        return Vector3.Distance(transform.position, _player.transform.position) <= _playerRange;
    }
}

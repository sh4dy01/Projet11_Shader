using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DamageableEnemy : DamageableEntity
{
    [SerializeField] private GameObject _damageZone;
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private float _playerRange = 5f;

    [Header("Drop Settings")]
    [SerializeField] private GameObject[] _itemToDrop;
    [SerializeField] [Range(0, 100)] private float _dropRate = 75;

    private NavMeshAgent _agent;
    private Transform _player;

    protected override void Awake()
    {
        base.Awake();
        
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _speed;
        
        CurrentOutlineMaterial = GameManager.Instance.EnemyOutlineMaterial;
        
        OnDeath += DropItem;
        OnDeath += DisableDamageZone;
        OnHealthUpdate += MoveToPlayer;
    }

    private void Update()
    {
        if (IsDead || !IsInRange()) return;

        MoveToPlayer();
    }
    
    private void MoveToPlayer()
    {
        if (IsDead) return;
        _agent.SetDestination(_player.position);
        //Rotate towards player
        Vector3 direction = (_player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    
    private void DropItem()
    {
        if (Random.Range(0, 100) <= _dropRate)
        {
            Instantiate(_itemToDrop[Random.Range(0, _itemToDrop.Length)], transform.position, Quaternion.identity);
        }
    }

    private void DisableDamageZone()
    {
        _damageZone.SetActive(false);
    }

    private bool IsInRange()
    {
        return Vector3.Distance(transform.position, _player.transform.position) <= _playerRange;
    }
}

using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour, ICheckRange
{
    public Transform player;
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private float _playerRange = 5f;
    
    private NavMeshAgent _agent;
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _speed;
    }

    private void Update()
    {
        if (IsInRange())
        {
            _agent.SetDestination(player.position);
        }
    }
    
    public bool IsInRange()
    {
        return Vector3.Distance(transform.position, player.transform.position) <= _playerRange;
    }
}

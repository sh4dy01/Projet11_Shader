// MoveTo.cs

using System;
using UnityEngine;
using UnityEngine.AI;
    
public class MoveTo : MonoBehaviour {
       
    public Transform goal;
    
    private NavMeshAgent _agent;
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update () {
        _agent.destination = goal.position; 
    }
}

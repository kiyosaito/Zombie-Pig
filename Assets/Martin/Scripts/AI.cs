// Title: AI
// Author(s): Martin Nguyen
// Date: 7/08/19
// Details: 
// Reference: 
// =========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    public GameObject player;
    public float attackDistance = 10f;
    public float followDistance = 20f;

    public Transform[] patrolPoints;
    private int currentPointIndex = 0;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
       
    }
    

    void Update()
    {
        if (_navMeshAgent.enabled)
        {
            float dist = Vector3.Distance(player.transform.position, this.transform.position);
            bool patrol = false;
            bool follow = (dist < followDistance);
            if (follow)
            {
                float random = Random.Range(0.0f,1.0f));
                _navMeshAgent.SetDestination(player.transform.position);
            }
        }
        
    }
    void NextPatrolPoint()
    {
        if (patrolPoints.Length>0)
        {
            _navMeshAgent.destination = patrolPoints[currentPointIndex].position;
            currentPointIndex++;
            currentPointIndex %= patrolPoints.Length;

        }
    }
}

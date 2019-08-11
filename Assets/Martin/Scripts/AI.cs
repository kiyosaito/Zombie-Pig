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
    private int _currentPointIndex = 0;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        MoveToNextPatrolPoint();
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
                float random = Random.Range(0.0f, 1.0f);
                _navMeshAgent.SetDestination(player.transform.position);
            }
            patrol = !follow && patrolPoints.Length > 0;
            if (!follow && !patrol)
            {
                _navMeshAgent.SetDestination(transform.position);
            }
            if (patrol)
            {
                if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance < 0.5f) 
                {
                    MoveToNextPatrolPoint();
                }
            }
        }
    }
    void MoveToNextPatrolPoint()
    {
        if (patrolPoints.Length > 0)
        {
            _navMeshAgent.destination = patrolPoints[_currentPointIndex].position;

            _currentPointIndex++;
            _currentPointIndex %= patrolPoints.Length;
        }
    }
    void NextPatrolPoint()
    {
        if (patrolPoints.Length>0)
        {
            _navMeshAgent.destination = patrolPoints[_currentPointIndex].position;
            _currentPointIndex++;
            _currentPointIndex %= patrolPoints.Length;

        }
    }
}

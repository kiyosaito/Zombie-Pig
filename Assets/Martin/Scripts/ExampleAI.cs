// Title: AI
// Author(s): Martin Nguyen
// Date: 13/08/19
// Details: 
// Reference: 
// =========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExampleAI : MonoBehaviour
{

    public enum State
    {
        Patrol, seek
    }

    #region Variables/References
    public State currentState;
    public Transform waypointParent;
    public float movespeed = 2f;
    public float stoppingDistance = 1f;

    public Transform[] waypoints;
    private int _currentIndex = 1;
    private NavMeshAgent agent;
    private Transform target;
    #endregion
    void Start()
    {
        waypoints = waypointParent.GetComponentsInChildren<Transform>();
        agent = GetComponent<NavMeshAgent>();
        currentState = State.Patrol;
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.seek:
                Seek();
                break;
            default:
                Patrol();
                break;
        }
    }

    #region functions
    void Patrol()
    {
        Transform point = waypoints[_currentIndex];

        float distance = Vector3.Distance(transform.position, point.position);

        if (distance < stoppingDistance)
        {
            _currentIndex++;
            if (_currentIndex >= waypoints.Length)
            {
                _currentIndex = 1;
            }
        }
        agent.SetDestination(point.position);
    }

    void Seek()
    {
        agent.SetDestination(target.position);
    }
    #endregion
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.transform;
            currentState = State.seek;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentState = State.Patrol;
        }
    }
}
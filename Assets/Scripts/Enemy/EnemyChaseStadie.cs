using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class EnemyChaseStadie : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;
    [SerializeField] private PlayableDirector PlayableDirector;
    [SerializeField] private LayerMask LayerMask;
    [SerializeField] private float _distance;
    private void Start()
    {
        agent.SetDestination(target.position);
    }

    private void Update()
    {
        if(Physics.CheckSphere(transform.position, _distance, LayerMask))
        {
            PlayableDirector.Play();
        }
        else
        {
            agent.SetDestination(target.position);
        }
    }
}

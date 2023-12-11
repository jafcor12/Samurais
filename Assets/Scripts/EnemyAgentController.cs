using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Windows;

public class EnemyAgentController : MonoBehaviour
{
    [SerializeField]
    Transform target;

    NavMeshAgent _agent;
    Animator _animator;

    bool _hitState;

    Collider colliderComponent;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        colliderComponent = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_hitState)
        {
            return;
        }
        _agent.SetDestination(target.position);
    }

    public void Die()
    {
        // Perform death animations, play sound, or destroy the enemy GameObject
        _hitState = true;
        _animator.SetBool("EnemyHit", true);
        colliderComponent.enabled = false;

        StartCoroutine(WaitDelete());
    }

    IEnumerator WaitDelete()
    {
        yield return new WaitForSeconds(10f);

        Destroy(gameObject);
    }
}


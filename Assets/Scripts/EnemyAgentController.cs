using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAgentController : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    int health;

    [SerializeField]
    LayerMask playerLayer;

    [SerializeField]
    public float radius;

    [SerializeField]
    [Range(0, 360)]
    public float angle;

    [SerializeField]
    public GameObject targetPlayer;

    [SerializeField]
    LayerMask targetMask;

    [SerializeField]
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    NavMeshAgent _agent;
    Animator _animator;

    bool _hitState;

    Collider colliderComponent;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        colliderComponent = GetComponent<Collider>();
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (health <= 0)
        {
            Die();
            if (_hitState)
            {
                return;
            }
        }

        FieldOfViewCheck();

        if (canSeePlayer)
        {
            _animator.SetBool("fieldOfVision", true);
            _agent.SetDestination(target.position);
        }
        else
            _animator.SetBool("fieldOfVision", false);
    }

    void Die()
    {
        _hitState = true;
        _animator.SetBool("EnemyHit", true);
        colliderComponent.enabled = false;

        StartCoroutine(WaitDelete());
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }

IEnumerator WaitDelete()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
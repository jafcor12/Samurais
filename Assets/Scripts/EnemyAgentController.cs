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
    float detectionRange = 5f;

    [SerializeField]
    int damage;

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
    bool isAlive = true;

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

        AttackDistance();
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
        if (isAlive && rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                    return;
                }
            }
        }
        canSeePlayer = false;

    }

    private void AttackDistance()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);
        if (isAlive && distanceToPlayer <= detectionRange)
        {
            _animator.SetBool("enemyAttack", true);

            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            if (!stateInfo.IsName("enemyAttack") && stateInfo.normalizedTime < 1.0f)
            {
                _animator.SetBool("enemyAttack", false);
            }
            return;
        }

        _animator.SetBool("enemyAttack", false);
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (other.CompareTag("Player"))
        {
            if (player != null)
            {
                isAlive = player.HandleDamage(damage);
            }
        }
    }

    IEnumerator WaitDelete()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}


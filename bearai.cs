using UnityEngine;
using UnityEngine.AI;

public class BearAI : MonoBehaviour
{
    public enum BearState
    {
        Idle,
        Walking,
        Chasing,
        Attacking,
        Dead
    }

    public BearState currentState = BearState.Idle;

    public Transform player;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float patrolRadius = 15f;
    public float minIdleTime = 2f;
    public float maxIdleTime = 5f;
    public float walkDistance = 5f;

    public Animator animator;
    public Avatar bearAvatar;

    private NavMeshAgent navAgent;
    private Vector3 patrolTarget;
    private float idleTimer = 0f;
    private float currentIdleTime = 0f;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        if (bearAvatar != null)
            animator.avatar = bearAvatar;

        navAgent = GetComponent<NavMeshAgent>();
        SetRandomIdleTime();
        currentState = BearState.Idle;
    }

    void Update()
    {
        if (currentState == BearState.Dead) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        bool isPlayerDetected = distanceToPlayer < detectionRange;

        switch (currentState)
        {
            case BearState.Idle:
                HandleIdleState(isPlayerDetected);
                break;
            case BearState.Walking:
                HandleWalkingState(isPlayerDetected);
                break;
            case BearState.Chasing:
                HandleChasingState(distanceToPlayer);
                break;
            case BearState.Attacking:
                HandleAttackingState();
                break;
        }
    }

    void HandleIdleState(bool isPlayerDetected)
    {
        SetAnimationState("Idle");
        idleTimer += Time.deltaTime;
        if (idleTimer >= currentIdleTime)
        {
            SetRandomPatrolTarget();
            navAgent.SetDestination(patrolTarget);
            currentState = BearState.Walking;
            idleTimer = 0f;
            SetRandomIdleTime();
        }
        if (isPlayerDetected)
            currentState = BearState.Chasing;
    }

    void HandleWalkingState(bool isPlayerDetected)
    {
        SetAnimationState("WalkBackward");
        if (!navAgent.pathPending && navAgent.remainingDistance < 0.5f)
            currentState = BearState.Idle;
        if (isPlayerDetected)
            currentState = BearState.Chasing;
    }

    void HandleChasingState(float distanceToPlayer)
    {
        SetAnimationState("Run Forward");
        navAgent.SetDestination(player.position);
        if (distanceToPlayer <= attackRange)
            currentState = BearState.Attacking;
    }

    void HandleAttackingState()
    {
        SetAnimationState("Idle");
        navAgent.isStopped = true;
        int randomAttack = Random.Range(1, 9);
        animator.SetTrigger("Attack " + randomAttack);
        Invoke("ResetAttack", 2f);
    }

    void SetAnimationState(string state)
    {
        animator.SetBool("Idle", state == "Idle");
        animator.SetBool("Run Forward", state == "Run Forward");
        animator.SetBool("WalkBackward", state == "WalkBackward");
    }

    void SetRandomPatrolTarget()
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkDistance + transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, walkDistance, NavMesh.AllAreas))
            patrolTarget = hit.position;
    }

    void SetRandomIdleTime()
    {
        currentIdleTime = Random.Range(minIdleTime, maxIdleTime);
    }

    void ResetAttack()
    {
        navAgent.isStopped = false;
        currentState = BearState.Idle;
    }
}

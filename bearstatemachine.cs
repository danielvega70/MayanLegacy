using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BearStateMachine : StateMachineBehaviour
{
    public enum BearState
    {
        Idle,
        Walking,
        Chasing,
        Attacking,
        Dead
    }

    public BearState CurrentState { get; private set; }

    private Animator _animator;
    private NavMeshAgent _navAgent;
    private Transform _player;
    private float _detectionRange;
    private float _attackRange;
    private float _patrolRadius;

    private Vector3 _patrolTarget;
    private bool _isPlayerDetected;

    public BearStateMachine(Animator animator, NavMeshAgent navAgent, Transform player, float detectionRange, float attackRange, float patrolRadius)
    {
        _animator = animator;
        _navAgent = navAgent;
        _player = player;
        _detectionRange = detectionRange;
        _attackRange = attackRange;
        _patrolRadius = patrolRadius;

        CurrentState = BearState.Idle;
        _isPlayerDetected = false;
    }

    public void Update(float deltaTime)
    {
        if (CurrentState == BearState.Dead) return;

        float distanceToPlayer = Vector3.Distance(_navAgent.transform.position, _player.position);
        _isPlayerDetected = distanceToPlayer < _detectionRange;

        switch (CurrentState)
        {
            case BearState.Idle:
                HandleIdleState();
                break;
            case BearState.Walking:
                HandleWalkingState();
                break;
            case BearState.Chasing:
                HandleChasingState(distanceToPlayer);
                break;
            case BearState.Attacking:
                HandleAttackingState();
                break;
        }
    }

    private void HandleIdleState()
    {
        _animator.SetBool("isIdle", true);
        _animator.SetBool("isWalking", false);
        _animator.SetBool("isChasing", false);
        _animator.SetBool("isAttacking", false);

        if (!_isPlayerDetected && !_navAgent.pathPending && _navAgent.remainingDistance < 0.5f)
        {
            SetRandomPatrolTarget();
            _navAgent.SetDestination(_patrolTarget);
            CurrentState = BearState.Walking;
        }
        else if (_isPlayerDetected)
        {
            CurrentState = BearState.Chasing;
        }
    }

    private void HandleWalkingState()
    {
        _animator.SetBool("isIdle", false);
        _animator.SetBool("isWalking", true);
        _animator.SetBool("isChasing", false);
        _animator.SetBool("isAttacking", false);

        if (!_navAgent.pathPending && _navAgent.remainingDistance < 0.5f)
        {
            SetRandomPatrolTarget();
            _navAgent.SetDestination(_patrolTarget);
        }

        if (_isPlayerDetected)
        {
            CurrentState = BearState.Chasing;
        }
    }

    private void HandleChasingState(float distanceToPlayer)
    {
        _animator.SetBool("isIdle", false);
        _animator.SetBool("isWalking", false);
        _animator.SetBool("isChasing", true);
        _animator.SetBool("isAttacking", false);

        _navAgent.SetDestination(_player.position);

        if (distanceToPlayer <= _attackRange)
        {
            CurrentState = BearState.Attacking;
        }
    }

    private void HandleAttackingState()
    {
        _animator.SetBool("isIdle", false);
        _animator.SetBool("isWalking", false);
        _animator.SetBool("isChasing", false);
        _animator.SetBool("isAttacking", true);

        _navAgent.isStopped = true;
        int randomAttack = Random.Range(1, 9);
        _animator.SetInteger("AttackType", randomAttack);
        _animator.SetTrigger("Attack");

        // Reset after attack
        System.Action resetAttack = () =>
        {
            _navAgent.isStopped = false;
            CurrentState = BearState.Idle;
            SetRandomPatrolTarget();
            _navAgent.SetDestination(_patrolTarget);
        };

        // Simulate a delay for the attack
        System.Threading.Tasks.Task.Delay(2000).ContinueWith(t => resetAttack());
    }

    private void SetRandomPatrolTarget()
    {
        Vector3 randomDirection = Random.insideUnitSphere * _patrolRadius;
        randomDirection += _navAgent.transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, _patrolRadius, 1);
        _patrolTarget = hit.position;
    }

    public void TakeDamage(string hitDirection)
    {
        _animator.SetTrigger($"GetHit{hitDirection}");
    }

    public void Die()
    {
        _animator.SetBool("isDead", true);
        CurrentState = BearState.Dead;
        _navAgent.isStopped = true;
    }
}

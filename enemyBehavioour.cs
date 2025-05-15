using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Transform = UnityEngine.Transform;

public class enemyAI : MonoBehaviour
{
    [Header("Player Detection")]
    public Transform player; // Referencia al jugador
    public float detectionRange = 10f; // Rango de detección
    public float attackRange = 2f; // Rango para atacar
    public float patrolRadius = 15f; // Radio de patrulla

    private NavMeshAgent navAgent;
    private Animator animator;
    private bool isPatrolling = true;
    private float patrolTimer = 0f;
    private float idleTimer = 0f;
    private bool isIdle = false;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        StartPatrolling();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform; // Detectar al jugador mediante el collider
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null; // Perder referencia al jugador al salir del rango
        }
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange)
            {
                navAgent.SetDestination(player.position);
                animator.SetBool("isWalking", true);

                if (distanceToPlayer <= attackRange)
                {
                    animator.SetBool("isAttacking", true);
                    navAgent.isStopped = true; // Detener movimiento para atacar
                }
                else
                {
                    animator.SetBool("isAttacking", false);
                    navAgent.isStopped = false;
                }
                return; // Salir de Update para evitar continuar con la patrulla
            }
        }

        PatrolLogic();
    }

    void StartPatrolling()
    {
        isPatrolling = true;
        SetNewPatrolDestination();
    }

    void SetNewPatrolDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, 1))
        {
            Vector3 finalPosition = hit.position;
            navAgent.SetDestination(finalPosition);
            animator.SetBool("isWalking", true);
            navAgent.isStopped = false;
        }
    }

    void PatrolLogic()
    {
        if (!isIdle)
        {
            patrolTimer += Time.deltaTime;
            if (patrolTimer >= 5f)
            {
                navAgent.isStopped = true;
                animator.SetBool("isWalking", false);
                isIdle = true;
                patrolTimer = 0f;
            }
        }
        else
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= 2f)
            {
                isIdle = false;
                idleTimer = 0f;
                SetNewPatrolDestination();
            }
        }
    }
}

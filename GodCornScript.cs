using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GodCornScript : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float detectionRange = 10f; // Rango de detección
    public float attackRange = 2f; // Rango para atacar
    public float patrolRadius = 15f; // Radio de patrulla

    private NavMeshAgent agent;
    private Animator animator;
    private bool isPatrolling = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        StartCoroutine(PatrolRoutine());
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            // Perseguir al jugador
            StopCoroutine(PatrolRoutine()); // Detener patrulla
            agent.SetDestination(player.position);
            animator.SetBool("isWalking", true);

            if (distanceToPlayer <= attackRange)
            {
                // Atacar si está cerca
                animator.SetBool("isKicking", true);
                agent.isStopped = true; // Detener movimiento para atacar
            }
            else
            {
                animator.SetBool("isKicking", false);
                agent.isStopped = false;
            }
        }
        else if (!isPatrolling)
        {
            // Reanudar patrulla si el jugador está lejos
            StartCoroutine(PatrolRoutine());
        }
    }

    IEnumerator PatrolRoutine()
    {
        isPatrolling = true;
        while (isPatrolling)
        {
            Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
            randomDirection += transform.position;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, 1))
            {
                Vector3 finalPosition = hit.position;
                agent.SetDestination(finalPosition);
                animator.SetBool("isWalking", true);
            }

            yield return new WaitForSeconds(5f); // Esperar 5 segundos antes de detenerse
            agent.isStopped = true;
            animator.SetBool("isWalking", false); // Animación de Idle
            yield return new WaitForSeconds(2f); // Tiempo de descanso en Idle
            agent.isStopped = false;
        }
    }
}

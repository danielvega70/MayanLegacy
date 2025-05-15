using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class npcmen : MonoBehaviour
{
    public float speed = 3.5f; // Velocidad del NPC
    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (agent != null)
        {
            agent.speed = speed;
            SetRandomDestination(); // Asigna un destino inicial
        }
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)


        {
            SetRandomDestination(); // Cambia de destino si ya llegó al anterior
        }

        // Actualiza la animación
        if (animator != null)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
    }

    void SetRandomDestination()
    {
        Vector3 randomPoint;
        if (RandomPoint(transform.position, 10f, out randomPoint)) // 10 es el radio de movimiento
        {
            agent.SetDestination(randomPoint);
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPos = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPos, out hit, range, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }
}

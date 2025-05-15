using UnityEngine;
using UnityEngine.AI;

public class Npcwalking : MonoBehaviour
{
    public float speed = 3.5f; // Velocidad del NPC
    public float detectionRange = 3f; // Rango para detectar al jugador
    public float stopTime = 3f; // Tiempo que se detendrá el NPC cuando vea al jugador

    private NavMeshAgent agent;
    private Animator animator;
    private bool isStopped = false;
    private float stopTimer = 0f;
    private Transform player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform; // Busca al jugador por tag

        if (agent != null)
        {
            agent.speed = speed;
            SetRandomDestination(); // Asigna un destino inicial
        }
    }

    void Update()
    {
        if (isStopped)
        {
            stopTimer += Time.deltaTime;
            if (stopTimer >= stopTime)
            {
                isStopped = false;
                agent.isStopped = false;
                SetRandomDestination(); // Vuelve a moverse
            }
            return;
        }

        if (player != null && Vector3.Distance(transform.position, player.position) < detectionRange)
        {
            StopNPC(); // Se detiene si el jugador está cerca
        }

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

    void StopNPC()
    {
        isStopped = true;
        stopTimer = 0f;
        agent.isStopped = true;
    }
}

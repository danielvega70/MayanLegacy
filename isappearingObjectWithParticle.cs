using System.Collections;
using UnityEngine;

public class DisappearingObjectWithParticle : MonoBehaviour
{
    [Header("Player Settings")]
    public Transform player; // Asigna el jugador en el inspector

    [Header("Object Settings")]
    public float radius = 1.0f; // La distancia m�xima permitida desde el jugador
    public float interval = 60f; // Intervalo de tiempo en segundos (1 minuto)

    [Header("Particle Settings")]
    public GameObject particleEffect; // Asigna la part�cula en el inspector
    private ParticleSystem particleSystem; // Referencia al sistema de part�culas

    private void Start()
    {
        // Obtener el componente de part�culas si no se asign� manualmente
        if (particleEffect != null)
        {
            particleSystem = particleEffect.GetComponent<ParticleSystem>();
        }

        // Comienza el ciclo de aparici�n/desaparici�n del objeto
        StartCoroutine(HandleObjectVisibility());
    }

    private IEnumerator HandleObjectVisibility()
    {
        while (true)
        {
            // Desaparecer el objeto y la part�cula
            gameObject.SetActive(false);
            if (particleSystem != null)
            {
                particleSystem.Stop(); // Detener la part�cula cuando el objeto desaparezca
            }
            yield return new WaitForSeconds(interval); // Esperar 1 minuto

            // Posicionar el objeto aleatoriamente alrededor del jugador dentro del radio
            Vector3 randomDirection = Random.insideUnitSphere; // Direcci�n aleatoria en 3D
            randomDirection.y = 0; // Mantener el objeto en el mismo nivel del suelo
            Vector3 targetPosition = player.position + randomDirection.normalized * radius;

            // Colocar el objeto en la nueva posici�n
            transform.position = targetPosition;

            // Hacer que el objeto aparezca y activar la part�cula
            gameObject.SetActive(true);
            if (particleSystem != null)
            {
                particleSystem.Play(); // Activar la part�cula cuando el objeto aparezca
            }

            yield return new WaitForSeconds(interval); // Esperar 1 minuto antes de desaparecer nuevamente
        }
    }

    private void Update()
    {
        // Asegurarse de que la part�cula siga la posici�n del objeto cuando se mueva
        if (particleEffect != null)
        {
            particleEffect.transform.position = transform.position;
        }
    }
}

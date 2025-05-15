using UnityEngine;

public class FireballShooter : MonoBehaviour
{
    public GameObject fireballPrefab;  // Prefab de la bola de fuego
    public Transform firePoint;        // Punto desde donde se disparan las bolas de fuego
    public Transform player;           // Referencia al jugador (puedes asignarlo manualmente)
    public float fireballSpeed = 10f;  // Velocidad de la bola de fuego
    public float fireRate = 2f;        // Tiempo entre disparos
    private float nextFireTime = 0f;   // Temporizador de disparo

    void Update()
    {
        if (player != null && Time.time >= nextFireTime)
        {
            ShootFireball(); // Disparar bola de fuego
            nextFireTime = Time.time + fireRate; // Reiniciar el temporizador
        }
    }

    void ShootFireball()
    {
        // Instanciar la bola de fuego en la posición del FirePoint
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);

        // Obtener el Rigidbody de la bola de fuego
        Rigidbody rb = fireball.GetComponent<Rigidbody>();

        // Calcular dirección hacia el jugador
        Vector3 direction = (player.position - firePoint.position).normalized;

        // Aplicar velocidad en dirección al jugador
        rb.velocity = direction * fireballSpeed;

        // Destruir la bola de fuego después de 5 segundos
        Destroy(fireball, 5f);
    }
}
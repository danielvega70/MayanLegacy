using UnityEngine;

public class CrossHairProjectileShooting : MonoBehaviour
{
    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public float projectileForce = 20f;
    public float projectileLifetime = 5f; // Tiempo antes de destruir el proyectil

    [Header("Shoot Point")]
    public Transform shootPoint; // El objeto que indica el punto y direcci�n de disparo

    [Header("Crosshair Settings")]
    public GameObject crosshairUI; // UI de la mira

    [Header("Audio Settings")]
    public AudioSource audioSource; // Fuente de audio
    public AudioClip shootSound;    // Sonido de disparo

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No se encontr� la c�mara principal.");
        }

        if (shootPoint == null)
        {
            Debug.LogError("No se asign� el punto de disparo.");
        }

        if (crosshairUI == null)
        {
            Debug.LogError("No se asign� el Crosshair UI.");
        }
    }

    void Update()
    {
        // Mover el crosshair a la posici�n del mouse
        if (crosshairUI != null)
        {
            Vector3 mousePos = Input.mousePosition;
            crosshairUI.transform.position = mousePos;
        }

        // Disparar al presionar bot�n derecho del mouse
        if (Input.GetMouseButtonDown(1))
        {
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        if (projectilePrefab == null || shootPoint == null) return;

        // Instanciar el proyectil en la posici�n y rotaci�n del shootPoint
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Lanzar el proyectil hacia el "forward" (flecha azul) del shootPoint
            rb.AddForce(shootPoint.forward * projectileForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("El proyectil no tiene Rigidbody asignado.");
        }

        // Reproducir sonido si existe
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        // Destruir proyectil despu�s de un tiempo para limpiar la escena
        Destroy(projectile, projectileLifetime);
    }
}
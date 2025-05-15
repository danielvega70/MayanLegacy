using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireblastere : MonoBehaviour
{
    public GameObject particlePrefab; // Prefab de la partícula que se disparará
    public Transform firePoint;       // Punto desde donde se disparará la partícula
    public float fireForce = 10f;     // Fuerza con la que se dispara la partícula

    void Update()
    {
        // Detectar si se presiona la tecla numérica "2"
        if (Input.GetKeyDown(KeyCode.Alpha2)) // Número 2 en el teclado principal
        {
            ShootParticle();
        }
    }

    void ShootParticle()
    {
        if (particlePrefab != null && firePoint != null)
        {
            // Crear la partícula en el punto de disparo
            GameObject particleInstance = Instantiate(particlePrefab, firePoint.position, firePoint.rotation);

            // Aplicar fuerza si la partícula tiene un Rigidbody
            Rigidbody rb = particleInstance.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(firePoint.forward * fireForce, ForceMode.Impulse);
            }

            // Autodestruir la partícula después de 5 segundos
            Destroy(particleInstance, 5f);
        }
        else
        {
            Debug.LogWarning("ParticlePrefab o FirePoint no asignado.");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireblastere : MonoBehaviour
{
    public GameObject particlePrefab; // Prefab de la part�cula que se disparar�
    public Transform firePoint;       // Punto desde donde se disparar� la part�cula
    public float fireForce = 10f;     // Fuerza con la que se dispara la part�cula

    void Update()
    {
        // Detectar si se presiona la tecla num�rica "2"
        if (Input.GetKeyDown(KeyCode.Alpha2)) // N�mero 2 en el teclado principal
        {
            ShootParticle();
        }
    }

    void ShootParticle()
    {
        if (particlePrefab != null && firePoint != null)
        {
            // Crear la part�cula en el punto de disparo
            GameObject particleInstance = Instantiate(particlePrefab, firePoint.position, firePoint.rotation);

            // Aplicar fuerza si la part�cula tiene un Rigidbody
            Rigidbody rb = particleInstance.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(firePoint.forward * fireForce, ForceMode.Impulse);
            }

            // Autodestruir la part�cula despu�s de 5 segundos
            Destroy(particleInstance, 5f);
        }
        else
        {
            Debug.LogWarning("ParticlePrefab o FirePoint no asignado.");
        }
    }
}
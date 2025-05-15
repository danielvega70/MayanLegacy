using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyexplosion : MonoBehaviour
{
    public ParticleSystem explosionEffect; // Asigna la partícula en el Inspector

    void Start()
    {
        if (explosionEffect != null)
        {
            explosionEffect.Play(); // Iniciar la explosión al comenzar el juego
        }
        else
        {
            Debug.LogWarning("[DEBUG] No se asignó una partícula de explosión.");
        }
    }
}
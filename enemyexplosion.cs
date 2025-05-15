using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyexplosion : MonoBehaviour
{
    public ParticleSystem explosionEffect; // Asigna la part�cula en el Inspector

    void Start()
    {
        if (explosionEffect != null)
        {
            explosionEffect.Play(); // Iniciar la explosi�n al comenzar el juego
        }
        else
        {
            Debug.LogWarning("[DEBUG] No se asign� una part�cula de explosi�n.");
        }
    }
}
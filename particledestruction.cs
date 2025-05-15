using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particledestruction : MonoBehaviour
{
    public float lifeTime = 5f; // Tiempo de vida en segundos

    void Start()
    {
        // Destruir este GameObject después del tiempo de vida especificado
        Destroy(gameObject, lifeTime);
    }
}
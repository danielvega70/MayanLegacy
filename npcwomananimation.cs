using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcwomananimation : MonoBehaviour
{
    private Animation npcAnimation;
    private Rigidbody npcRigidbody;

    [Header("Animation Settings")]
    public string animationName = "IdleAnimation";  // Nombre de la animaci�n a reproducir

    void Awake()
    {
        // Obtener el componente de Rigidbody y Animation
        npcRigidbody = GetComponent<Rigidbody>();
        npcAnimation = GetComponent<Animation>();

        // Verificar que los componentes existen
        if (npcRigidbody == null)
        {
            Debug.LogError("El componente de Rigidbody no est� presente en el NPC.");
        }

        if (npcAnimation == null)
        {
            Debug.LogError("El componente de Animation no est� presente en el NPC.");
        }
    }

    void Start()
    {
        // Configurar Rigidbody a Kinematic para que las f�sicas no afecten al NPC
        if (npcRigidbody != null)
        {
            npcRigidbody.isKinematic = true;  // Establece el Rigidbody como Kinematic
        }

        // Verificar si la animaci�n est� configurada correctamente
        if (npcAnimation != null)
        {
            // Asegurarse de que la animaci�n est� en bucle
            npcAnimation.wrapMode = WrapMode.Loop;  // Establecer la animaci�n para que est� en bucle

            // Reproducir la animaci�n al iniciar
            if (!string.IsNullOrEmpty(animationName))
            {
                npcAnimation.Play(animationName);  // Reproducir la animaci�n seleccionada en el Inspector
            }
            else
            {
                Debug.LogError("No se ha especificado un nombre de animaci�n.");
            }
        }
    }

    void Update()
    {
        // Aqu� puedes agregar otros comportamientos si es necesario
    }
}
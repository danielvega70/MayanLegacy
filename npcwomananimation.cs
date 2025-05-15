using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcwomananimation : MonoBehaviour
{
    private Animation npcAnimation;
    private Rigidbody npcRigidbody;

    [Header("Animation Settings")]
    public string animationName = "IdleAnimation";  // Nombre de la animación a reproducir

    void Awake()
    {
        // Obtener el componente de Rigidbody y Animation
        npcRigidbody = GetComponent<Rigidbody>();
        npcAnimation = GetComponent<Animation>();

        // Verificar que los componentes existen
        if (npcRigidbody == null)
        {
            Debug.LogError("El componente de Rigidbody no está presente en el NPC.");
        }

        if (npcAnimation == null)
        {
            Debug.LogError("El componente de Animation no está presente en el NPC.");
        }
    }

    void Start()
    {
        // Configurar Rigidbody a Kinematic para que las físicas no afecten al NPC
        if (npcRigidbody != null)
        {
            npcRigidbody.isKinematic = true;  // Establece el Rigidbody como Kinematic
        }

        // Verificar si la animación está configurada correctamente
        if (npcAnimation != null)
        {
            // Asegurarse de que la animación esté en bucle
            npcAnimation.wrapMode = WrapMode.Loop;  // Establecer la animación para que esté en bucle

            // Reproducir la animación al iniciar
            if (!string.IsNullOrEmpty(animationName))
            {
                npcAnimation.Play(animationName);  // Reproducir la animación seleccionada en el Inspector
            }
            else
            {
                Debug.LogError("No se ha especificado un nombre de animación.");
            }
        }
    }

    void Update()
    {
        // Aquí puedes agregar otros comportamientos si es necesario
    }
}
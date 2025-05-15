using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpeach : MonoBehaviour
{
    [Tooltip("Transform del personaje o de la parte del personaje que se debe seguir (por ejemplo, la cabeza).")]
    public Transform target;

    [Tooltip("Canvas donde se encuentra la bubble speech. Aseg�rate de que est� en modo World Space.")]
    public Canvas canvas;

    [Tooltip("Desplazamiento local respecto al target para posicionar la burbuja (por ejemplo, para que aparezca sobre la cabeza).")]
    public Vector3 offset = new Vector3(0, 2, 0);

    void Start()
    {
        // Si se asign� un Canvas y este objeto no es hijo del Canvas, se establece la relaci�n
        if (canvas != null && transform.parent != canvas.transform)
        {
            transform.SetParent(canvas.transform, false);
        }
    }

    void Update()
    {
        if (target != null)
        {
            // Actualiza la posici�n de la burbuja sumando el offset al target
            transform.position = target.position + offset;

            // Opcional: hacer que la burbuja siempre mire hacia la c�mara para mejorar la legibilidad
            if (Camera.main != null)
            {
                transform.LookAt(Camera.main.transform);
                transform.Rotate(0, 180f, 0); // Ajuste para que quede de frente a la c�mara
            }
        }
    }
}
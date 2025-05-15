using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracollision : MonoBehaviour
{
    [Header("Camera Collider Settings")]
    public Collider cameraCollider; // Collider que se asignar� a la c�mara, se puede elegir desde el Inspector
    public Collider targetCollider; // Collider con el que la c�mara colisionar�, tambi�n asignado desde el Inspector

    [Header("Camera Settings")]
    public float minDistance = 0.5f;  // Distancia m�nima que la c�mara puede estar del objeto
    public float maxDistance = 5f;    // Distancia m�xima que la c�mara puede moverse hacia el objetivo

    private Rigidbody rb;

    void Start()
    {
        // Verificar si el collider de la c�mara ha sido asignado en el inspector
        if (cameraCollider == null)
        {
            Debug.LogError("No se ha asignado un Collider a la c�mara. Asignalo en el Inspector.");
        }

        // Verificar si el collider del objetivo ha sido asignado
        if (targetCollider == null)
        {
            Debug.LogError("No se ha asignado un Collider del objetivo en el Inspector.");
        }

        // Aseg�rate de que la c�mara tenga un Rigidbody
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.isKinematic = true; // Asegura que no est� afectado por fuerzas f�sicas
    }

    void Update()
    {
        // Si la c�mara tiene collider y un collider de destino, puedes empezar a detectar colisiones
        if (cameraCollider != null && targetCollider != null)
        {
            DetectCollision();
        }
    }

    private void DetectCollision()
    {
        // Si la distancia entre la c�mara y el objeto colisiona con el collider del objetivo, ajusta la c�mara
        if (Vector3.Distance(cameraCollider.transform.position, targetCollider.transform.position) < minDistance)
        {
            // Ajuste de la c�mara si colisiona (esto lo puedes ajustar seg�n lo que quieras hacer)
            Vector3 direction = cameraCollider.transform.position - targetCollider.transform.position;
            cameraCollider.transform.position += direction.normalized * minDistance;
            Debug.Log("La c�mara ha colisionado con el objetivo. Ajustando la posici�n.");
        }
    }

    // M�todo para manejar la colisi�n entre la c�mara y el collider del objetivo
    private void OnCollisionEnter(Collision collision)
    {
        // Solo se activa si colisiona con el collider del objetivo asignado en el inspector
        if (collision.collider == targetCollider)
        {
            Debug.Log("Colisi�n detectada entre la c�mara y el objetivo.");
            // Aqu� puedes realizar cualquier l�gica adicional, como detener el movimiento de la c�mara, etc.
        }
    }
}
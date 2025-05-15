using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracollision : MonoBehaviour
{
    [Header("Camera Collider Settings")]
    public Collider cameraCollider; // Collider que se asignará a la cámara, se puede elegir desde el Inspector
    public Collider targetCollider; // Collider con el que la cámara colisionará, también asignado desde el Inspector

    [Header("Camera Settings")]
    public float minDistance = 0.5f;  // Distancia mínima que la cámara puede estar del objeto
    public float maxDistance = 5f;    // Distancia máxima que la cámara puede moverse hacia el objetivo

    private Rigidbody rb;

    void Start()
    {
        // Verificar si el collider de la cámara ha sido asignado en el inspector
        if (cameraCollider == null)
        {
            Debug.LogError("No se ha asignado un Collider a la cámara. Asignalo en el Inspector.");
        }

        // Verificar si el collider del objetivo ha sido asignado
        if (targetCollider == null)
        {
            Debug.LogError("No se ha asignado un Collider del objetivo en el Inspector.");
        }

        // Asegúrate de que la cámara tenga un Rigidbody
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.isKinematic = true; // Asegura que no esté afectado por fuerzas físicas
    }

    void Update()
    {
        // Si la cámara tiene collider y un collider de destino, puedes empezar a detectar colisiones
        if (cameraCollider != null && targetCollider != null)
        {
            DetectCollision();
        }
    }

    private void DetectCollision()
    {
        // Si la distancia entre la cámara y el objeto colisiona con el collider del objetivo, ajusta la cámara
        if (Vector3.Distance(cameraCollider.transform.position, targetCollider.transform.position) < minDistance)
        {
            // Ajuste de la cámara si colisiona (esto lo puedes ajustar según lo que quieras hacer)
            Vector3 direction = cameraCollider.transform.position - targetCollider.transform.position;
            cameraCollider.transform.position += direction.normalized * minDistance;
            Debug.Log("La cámara ha colisionado con el objetivo. Ajustando la posición.");
        }
    }

    // Método para manejar la colisión entre la cámara y el collider del objetivo
    private void OnCollisionEnter(Collision collision)
    {
        // Solo se activa si colisiona con el collider del objetivo asignado en el inspector
        if (collision.collider == targetCollider)
        {
            Debug.Log("Colisión detectada entre la cámara y el objetivo.");
            // Aquí puedes realizar cualquier lógica adicional, como detener el movimiento de la cámara, etc.
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWallCollision : MonoBehaviour
{
    public Transform player;               // El punto desde donde se lanza el raycast (normalmente la cabeza o cuerpo del jugador)
    public float minDistance = 1.0f;       // Distancia mínima a la que puede estar la cámara
    public float maxDistance = 4.0f;       // Distancia máxima (posición normal de la cámara)
    public LayerMask collisionLayers;      // Capas con las que queremos detectar colisión (como terreno, paredes, etc.)
    public float smoothSpeed = 10f;        // Suavidad al mover la cámara

    private Vector3 currentVelocity;

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position - transform.forward * maxDistance;

        // Lanza un raycast desde el jugador hacia la cámara
        if (Physics.Raycast(player.position, -transform.forward, out RaycastHit hit, maxDistance, collisionLayers))
        {
            // Si colisiona, posicionamos la cámara justo antes de ese punto
            desiredPosition = player.position - transform.forward * Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }

        // Mover la cámara suavemente hacia la posición deseada
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, 1 / smoothSpeed);
    }
}
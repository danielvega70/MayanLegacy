using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWallCollision : MonoBehaviour
{
    public Transform player;               // El punto desde donde se lanza el raycast (normalmente la cabeza o cuerpo del jugador)
    public float minDistance = 1.0f;       // Distancia m�nima a la que puede estar la c�mara
    public float maxDistance = 4.0f;       // Distancia m�xima (posici�n normal de la c�mara)
    public LayerMask collisionLayers;      // Capas con las que queremos detectar colisi�n (como terreno, paredes, etc.)
    public float smoothSpeed = 10f;        // Suavidad al mover la c�mara

    private Vector3 currentVelocity;

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position - transform.forward * maxDistance;

        // Lanza un raycast desde el jugador hacia la c�mara
        if (Physics.Raycast(player.position, -transform.forward, out RaycastHit hit, maxDistance, collisionLayers))
        {
            // Si colisiona, posicionamos la c�mara justo antes de ese punto
            desiredPosition = player.position - transform.forward * Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }

        // Mover la c�mara suavemente hacia la posici�n deseada
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, 1 / smoothSpeed);
    }
}
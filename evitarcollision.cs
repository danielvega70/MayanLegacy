using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class evitarcollision : MonoBehaviour
{
    public float radioColision = 0.3f;
    public float alturaMinima = 0.1f; // Altura m�nima permitida sobre el suelo
    public LayerMask capaGround; // Layer del suelo
    public Collider jugadorCollider; // Aqu� arrastras el Collider del jugador
    public bool limitarRotacion = true; // Activar o no el l�mite de rotaci�n
    public float limiteInferiorRotacion = 80f; // L�mite m�ximo para mirar hacia abajo

    void LateUpdate()
    {
        Vector3 posicionCamara = transform.position;
        Vector3 direccionAbajo = Vector3.down;

        // Hacemos un SphereCast solo contra Ground, pero luego ignoramos el Collider del jugador manualmente
        if (Physics.SphereCast(posicionCamara, radioColision, direccionAbajo, out RaycastHit hit, radioColision + 0.1f, capaGround, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider != jugadorCollider) // Si el golpeado NO es el jugador
            {
                float distanciaASuelo = hit.distance;

                float distanciaASubir = (radioColision + alturaMinima) - distanciaASuelo;
                if (distanciaASubir > 0f)
                {
                    transform.position += Vector3.up * distanciaASubir;
                }
            }
        }

        // Limitar rotaci�n vertical si est� activado
        if (limitarRotacion)
        {
            Vector3 rot = transform.eulerAngles;

            if (rot.x > limiteInferiorRotacion && rot.x < 180f)
            {
                rot.x = limiteInferiorRotacion;
                transform.eulerAngles = rot;
            }
        }
    }
}

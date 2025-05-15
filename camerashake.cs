using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerashake : MonoBehaviour
{
    public Camera playerCamera;      // Cámara que se sacudirá
    public float shakeAmount = 0.1f; // Cantidad de sacudida
    public float shakeDuration = 0.2f; // Duración de la sacudida

    private Vector3 originalPosition; // Posición original de la cámara

    private void Start()
    {
        // Guardamos la posición original de la cámara
        if (playerCamera == null)
        {
            playerCamera = Camera.main; // Si no hay cámara asignada, usamos la cámara principal
        }

        originalPosition = playerCamera.transform.localPosition;
    }

    // Llamar este método para activar la sacudida
    public void TriggerShake()
    {
        // Iniciamos la corutina para aplicar la sacudida
        StartCoroutine(Shake());
    }

    // Corutina para la sacudida
    private IEnumerator Shake()
    {
        float timeElapsed = 0f;

        // Mientras quede tiempo para la sacudida
        while (timeElapsed < shakeDuration)
        {
            // Generar un desplazamiento aleatorio en la cámara
            float x = Random.Range(-1f, 1f) * shakeAmount;
            float y = Random.Range(-1f, 1f) * shakeAmount;

            // Aplicar el desplazamiento en la posición de la cámara
            playerCamera.transform.localPosition = new Vector3(x, y, originalPosition.z);

            timeElapsed += Time.deltaTime;

            // Esperamos un frame antes de seguir
            yield return null;
        }

        // Volver a la posición original de la cámara
        playerCamera.transform.localPosition = originalPosition;
    }
}

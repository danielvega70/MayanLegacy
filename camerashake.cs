using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerashake : MonoBehaviour
{
    public Camera playerCamera;      // C�mara que se sacudir�
    public float shakeAmount = 0.1f; // Cantidad de sacudida
    public float shakeDuration = 0.2f; // Duraci�n de la sacudida

    private Vector3 originalPosition; // Posici�n original de la c�mara

    private void Start()
    {
        // Guardamos la posici�n original de la c�mara
        if (playerCamera == null)
        {
            playerCamera = Camera.main; // Si no hay c�mara asignada, usamos la c�mara principal
        }

        originalPosition = playerCamera.transform.localPosition;
    }

    // Llamar este m�todo para activar la sacudida
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
            // Generar un desplazamiento aleatorio en la c�mara
            float x = Random.Range(-1f, 1f) * shakeAmount;
            float y = Random.Range(-1f, 1f) * shakeAmount;

            // Aplicar el desplazamiento en la posici�n de la c�mara
            playerCamera.transform.localPosition = new Vector3(x, y, originalPosition.z);

            timeElapsed += Time.deltaTime;

            // Esperamos un frame antes de seguir
            yield return null;
        }

        // Volver a la posici�n original de la c�mara
        playerCamera.transform.localPosition = originalPosition;
    }
}

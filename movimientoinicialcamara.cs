using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoinicialcamara : MonoBehaviour
{
    public Transform destino;       // Posición y rotación final
    public float duracion = 10f;    // Tiempo total del movimiento en segundos

    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;
    private float tiempoTranscurrido = 0f;
    private bool moviendo = true;

    void Start()
    {
        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;
    }

    void Update()
    {
        if (!moviendo) return;

        tiempoTranscurrido += Time.deltaTime;
        float t = Mathf.Clamp01(tiempoTranscurrido / duracion);

        // Usamos una curva de suavizado mejor que SmoothStep
        float tSuavizado = EaseInOutCubic(t);

        transform.position = Vector3.Lerp(posicionInicial, destino.position, tSuavizado);
        transform.rotation = Quaternion.Slerp(rotacionInicial, destino.rotation, tSuavizado);

        if (t >= 1f)
        {
            moviendo = false;
            // Aseguramos que termine exactamente en el destino (por si quedó un pequeño error)
            transform.position = destino.position;
            transform.rotation = destino.rotation;
        }
    }

    // Función de suavizado tipo "EaseInOutCubic"
    private float EaseInOutCubic(float x)
    {
        return x < 0.5f
            ? 4f * x * x * x
            : 1f - Mathf.Pow(-2f * x + 2f, 3f) / 2f;
    }
}

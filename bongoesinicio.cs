using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bongoesinicio : MonoBehaviour
{
    [Header("Configuración de Audio")]
    public AudioClip pistaMusical;
    public float volumenMaximo = 1.0f; // El volumen máximo que alcanzará
    public float tiempoDeSubida = 10.0f; // Tiempo que tarda en llegar al volumen máximo (en segundos)

    private AudioSource audioSource;
    private float tiempoTranscurrido = 0f;
    private bool subiendoVolumen = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (pistaMusical != null)
        {
            audioSource.clip = pistaMusical;
            audioSource.volume = 0f;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No se ha asignado ninguna pista musical.");
        }
    }

    void Update()
    {
        if (subiendoVolumen && audioSource.isPlaying)
        {
            tiempoTranscurrido += Time.deltaTime;
            float progreso = Mathf.Clamp01(tiempoTranscurrido / tiempoDeSubida);
            audioSource.volume = Mathf.Lerp(0f, volumenMaximo, progreso);

            if (progreso >= 1f)
            {
                subiendoVolumen = false; // Ya alcanzó el volumen máximo
            }
        }
    }
}
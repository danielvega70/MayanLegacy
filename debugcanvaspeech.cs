
using UnityEngine;

public class debugcanvaspeech : MonoBehaviour
{
    public GameObject speechBubble; // El SpeechBubble (arrástralo en el Inspector)
    public Transform speechPoint; // El punto de referencia donde el SpeechBubble debe seguir al personaje
    private Canvas canvas;
    private RectTransform speechBubbleRect;

    void Start()
    {
        // Asegurarse de que el Canvas esté bien asignado
        canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            Debug.Log("Canvas encontrado: " + canvas.name);
            Debug.Log("Canvas Render Mode: " + canvas.renderMode);
            if (canvas.renderMode == RenderMode.WorldSpace)
            {
                Debug.Log("Canvas está en el modo WorldSpace.");
            }
            else
            {
                Debug.Log("Canvas está en el modo Overlay.");
            }
        }
        else
        {
            Debug.LogWarning("No se encontró un Canvas en los padres del objeto.");
        }

        // Asegurarse de que speechBubble tenga un RectTransform para obtener su tamaño
        if (speechBubble != null)
        {
            speechBubbleRect = speechBubble.GetComponent<RectTransform>();
            if (speechBubbleRect != null)
            {
                Debug.Log("RectTransform de SpeechBubble encontrado.");
            }
            else
            {
                Debug.LogWarning("No se encontró un RectTransform en SpeechBubble.");
            }
        }
        else
        {
            Debug.LogWarning("No se ha asignado el SpeechBubble en el Inspector.");
        }
    }

    void Update()
    {
        // Solo si el SpeechBubble está activo, vamos a mostrar información útil
        if (speechBubble != null && speechBubble.activeSelf)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(speechPoint.position);
            speechBubble.transform.position = screenPos;

            // Información de posición en pantalla
            Debug.Log("Posición SpeechBubble (pantalla): " + screenPos);

            // Tamaño actual del SpeechBubble
            if (speechBubbleRect != null)
            {
                Debug.Log("Tamaño actual del SpeechBubble: " + speechBubbleRect.sizeDelta);
            }
        }

        // Mostrar información constante sobre el estado del SpeechBubble
        if (speechBubble != null)
        {
            Debug.Log("SpeechBubble Activo: " + speechBubble.activeSelf);
        }
    }
}
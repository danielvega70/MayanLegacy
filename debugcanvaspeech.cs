
using UnityEngine;

public class debugcanvaspeech : MonoBehaviour
{
    public GameObject speechBubble; // El SpeechBubble (arr�stralo en el Inspector)
    public Transform speechPoint; // El punto de referencia donde el SpeechBubble debe seguir al personaje
    private Canvas canvas;
    private RectTransform speechBubbleRect;

    void Start()
    {
        // Asegurarse de que el Canvas est� bien asignado
        canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            Debug.Log("Canvas encontrado: " + canvas.name);
            Debug.Log("Canvas Render Mode: " + canvas.renderMode);
            if (canvas.renderMode == RenderMode.WorldSpace)
            {
                Debug.Log("Canvas est� en el modo WorldSpace.");
            }
            else
            {
                Debug.Log("Canvas est� en el modo Overlay.");
            }
        }
        else
        {
            Debug.LogWarning("No se encontr� un Canvas en los padres del objeto.");
        }

        // Asegurarse de que speechBubble tenga un RectTransform para obtener su tama�o
        if (speechBubble != null)
        {
            speechBubbleRect = speechBubble.GetComponent<RectTransform>();
            if (speechBubbleRect != null)
            {
                Debug.Log("RectTransform de SpeechBubble encontrado.");
            }
            else
            {
                Debug.LogWarning("No se encontr� un RectTransform en SpeechBubble.");
            }
        }
        else
        {
            Debug.LogWarning("No se ha asignado el SpeechBubble en el Inspector.");
        }
    }

    void Update()
    {
        // Solo si el SpeechBubble est� activo, vamos a mostrar informaci�n �til
        if (speechBubble != null && speechBubble.activeSelf)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(speechPoint.position);
            speechBubble.transform.position = screenPos;

            // Informaci�n de posici�n en pantalla
            Debug.Log("Posici�n SpeechBubble (pantalla): " + screenPos);

            // Tama�o actual del SpeechBubble
            if (speechBubbleRect != null)
            {
                Debug.Log("Tama�o actual del SpeechBubble: " + speechBubbleRect.sizeDelta);
            }
        }

        // Mostrar informaci�n constante sobre el estado del SpeechBubble
        if (speechBubble != null)
        {
            Debug.Log("SpeechBubble Activo: " + speechBubble.activeSelf);
        }
    }
}
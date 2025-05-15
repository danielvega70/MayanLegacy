using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TMPro;

public class adjustcanvatospeechbubble : MonoBehaviour
{
    public RectTransform speechBubbleRect; // El RectTransform del SpeechBubble
    public RectTransform canvasRect; // El RectTransform del Canvas
    public TextMeshProUGUI textComponent; // Texto dentro del SpeechBubble

    void Start()
    {
        AdjustSize();
    }

    void Update()
    {
        AdjustSize(); // Se ajusta dinámicamente en caso de que el texto cambie
    }

    void AdjustSize()
    {
        if (speechBubbleRect == null || canvasRect == null || textComponent == null)
        {
            Debug.LogWarning("Faltan referencias en AdjustCanvasToSpeechBubble. ¡Asignálas en el Inspector, flojito! 😜");
            return;
        }

        // Ajustar el tamaño del Canvas al tamaño del SpeechBubble
        Vector2 bubbleSize = speechBubbleRect.sizeDelta;
        canvasRect.sizeDelta = bubbleSize;

        // Opcional: Si quieres que el SpeechBubble crezca según el texto
        float textWidth = textComponent.preferredWidth + 20f; // Espaciado extra
        float textHeight = textComponent.preferredHeight + 20f;

        speechBubbleRect.sizeDelta = new Vector2(textWidth, textHeight);
    }
}
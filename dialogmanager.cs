using UnityEngine;
using TMPro;  // Asegúrate de tener TextMeshPro instalado

public class DialogueManager : MonoBehaviour
{
    [Header("Configuración de Speech Bubble")]
    [Tooltip("GameObject que contiene la Speech Bubble (con su Canvas interno).")]
    public GameObject speechBubble;
    [Tooltip("Punto de referencia (por ejemplo, la cabeza del personaje) para posicionar la Speech Bubble.")]
    public Transform speechPoint;
    [Tooltip("Canvas personalizado para la Speech Bubble (opcional).")]
    public Canvas customCanvas;
    [Tooltip("Arreglo con los diálogos a mostrar.")]
    public string[] dialogue = new string[4];
    [Tooltip("Tamaño máximo permitido para el texto dentro de la burbuja.")]
    public float maxFontSize = 24f;

    private Canvas speechBubbleCanvas;
    private TextMeshProUGUI textComponent;
    private RectTransform speechBubbleRect;
    private float timeSpent = 0f; // Temporizador para gestionar el tiempo de visualización
    private int currentDialogueIndex = 0; // Índice del diálogo actual
    private bool isShowing = false; // Indica si la Speech Bubble está visible actualmente

    void Start()
    {
        // Verificar que se haya asignado la Speech Bubble
        if (speechBubble != null)
        {
            // Buscar el Canvas y el componente TextMeshProUGUI dentro de la Speech Bubble
            speechBubbleCanvas = speechBubble.GetComponentInChildren<Canvas>();
            textComponent = speechBubble.GetComponentInChildren<TextMeshProUGUI>();
            speechBubbleRect = speechBubble.GetComponent<RectTransform>();

            if (speechBubbleCanvas != null)
            {
                Debug.Log("Canvas encontrado dentro del SpeechBubble.");
            }
            else
            {
                Debug.LogWarning("No se encontró un Canvas dentro del SpeechBubble.");
            }
        }
        else
        {
            Debug.LogError("speechBubble no asignado en el Inspector.");
        }

        // Si se asigna un Canvas personalizado, éste reemplaza al encontrado
        if (customCanvas != null)
        {
            speechBubbleCanvas = customCanvas;
            Debug.Log("Canvas personalizado seleccionado: " + customCanvas.name);
        }

        // Verificar que se encontró el componente TextMeshProUGUI
        if (textComponent != null)
        {
            Debug.Log("TextMeshProUGUI encontrado dentro del SpeechBubble.");
        }
        else
        {
            Debug.LogWarning("No se encontró TextMeshProUGUI dentro del SpeechBubble.");
        }

        // Inicializar la Speech Bubble como oculta
        speechBubble.SetActive(false);

        // Si no se han asignado diálogos o el primero está vacío, usar diálogos predeterminados
        if (dialogue.Length == 0 || string.IsNullOrEmpty(dialogue[0]))
        {
            dialogue = new string[4] {
                "¡Los dioses se enojan! ¿Qué estás haciendo?",
                "Has perturbado el equilibrio sagrado.",
                "La ira de los dioses es inevitable.",
                "Te he advertido, joven guerrero, ¡el juicio se acerca!"
            };
        }

        // Mostrar el primer diálogo al iniciar y marcar la Speech Bubble como visible
        ShowNextDialogue();
        isShowing = true;
    }

    void Update()
    {
        // Actualizar el temporizador
        timeSpent += Time.deltaTime;

        // Gestionar la visibilidad de la Speech Bubble:
        // Se muestra durante 10 segundos, luego se oculta por 2 segundos y se muestra el siguiente diálogo
        if (isShowing)
        {
            if (timeSpent >= 10f) // Mostrar durante 10 segundos
            {
                speechBubble.SetActive(false); // Ocultar después de 10 segundos
                timeSpent = 0f;
                isShowing = false;
            }
        }
        else
        {
            if (timeSpent >= 2f) // Pausar durante 2 segundos
            {
                ShowNextDialogue(); // Mostrar el siguiente diálogo
                timeSpent = 0f;
                isShowing = true;
            }
        }

        // Mover la Speech Bubble para que siga la posición del speechPoint
        if (speechBubble.activeSelf)
        {
            // Si el Canvas es de tipo Screen Space, se convierte la posición del mundo a posición de pantalla
            if (speechBubbleCanvas != null && (speechBubbleCanvas.renderMode == RenderMode.ScreenSpaceOverlay ||
                                                 speechBubbleCanvas.renderMode == RenderMode.ScreenSpaceCamera))
            {
                Vector3 screenPos = Camera.main.WorldToScreenPoint(speechPoint.position);
                speechBubble.transform.position = screenPos;
            }
            else
            {
                // Para Canvas en World Space, se asigna la posición directamente en el mundo
                speechBubble.transform.position = speechPoint.position;
            }
        }
    }

    private void ShowNextDialogue()
    {
        // Activar la Speech Bubble y actualizar el texto con el diálogo actual
        speechBubble.SetActive(true);

        if (textComponent != null)
        {
            // Ajustar el tamaño del texto al máximo permitido
            textComponent.text = dialogue[currentDialogueIndex];

            // Ajustar el tamaño del texto para que no se desborde de la burbuja
            float textWidth = textComponent.preferredWidth;
            float bubbleWidth = speechBubbleRect.rect.width;

            // Si el texto es más grande que el área de la burbuja, ajustar el tamaño del texto
            if (textWidth > bubbleWidth)
            {
                textComponent.fontSize = Mathf.Min(maxFontSize, textComponent.fontSize * (bubbleWidth / textWidth));
            }

            // Asegurar que el texto no exceda el tamaño máximo especificado
            textComponent.fontSize = Mathf.Min(maxFontSize, textComponent.fontSize);
        }
        else
        {
            Debug.LogWarning("El componente TextMeshProUGUI no está asignado.");
        }

        // Actualizar el índice del diálogo actual para la próxima vez
        currentDialogueIndex = (currentDialogueIndex + 1) % dialogue.Length;
    }
}
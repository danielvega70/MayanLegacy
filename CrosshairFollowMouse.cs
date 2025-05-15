using UnityEngine;
using UnityEngine.UI;
public class CrosshairFollowMouse : MonoBehaviour
{
    public RectTransform crosshair; // Referencia a la Raw Image del crosshair

    void Start()
    {
        // Ocultar el cursor del sistema para usar solo el crosshair
        Cursor.visible = false;
    }

    void Update()
    {
        if (crosshair != null)
        {
            // Convertir la posición del mouse en coordenadas de pantalla
            Vector2 mousePosition = Input.mousePosition;
            crosshair.position = mousePosition;
        }
    }
}
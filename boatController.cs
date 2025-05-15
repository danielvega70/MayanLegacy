using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatController : MonoBehaviour
{
    [Header("Botes Configurables")]
    public GameObject[] botes; // Aquí se asignarán los 4 botes
    public GameObject jugador; // Aquí se asignará al jugador

    [Header("Control del Movimiento")]
    public float velocidadBote = 5f; // Velocidad del bote
    public float rangoInteraccion = 3f; // Rango de interacción para subirse al bote
    private bool enBote = false; // Controla si el jugador está dentro de un bote
    private GameObject boteSeleccionado = null; // El bote que el jugador ha tocado
    private Vector3 puntoInteraccion; // El punto en el bote donde se sube el jugador

    private Rigidbody rbBote; // Rigidbody del bote para controlar la física

    void Start()
    {
        // Asegúrate de que cada bote tenga un Rigidbody para moverse con física
        foreach (GameObject bote in botes)
        {
            rbBote = bote.GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        if (enBote && boteSeleccionado != null)
        {
            MoverBoteConJugador(); // Si está en el bote, moverlo con el bote
        }
        else
        {
            RevisarInteraccion(); // Si no está en el bote, buscar un bote cerca
        }
    }

    private void RevisarInteraccion()
    {
        // Verificar si el jugador está cerca de algún bote
        foreach (GameObject bote in botes)
        {
            float distancia = Vector3.Distance(jugador.transform.position, bote.transform.position);
            if (distancia <= rangoInteraccion)
            {
                // Si está cerca, se puede interactuar
                if (Input.GetKeyDown(KeyCode.E)) // Cambia esta tecla a la que prefieras
                {
                    enBote = true;
                    boteSeleccionado = bote;
                    puntoInteraccion = bote.transform.position; // El jugador se subirá al punto de interacción
                    jugador.transform.position = puntoInteraccion; // Mueve al jugador al punto de interacción
                    jugador.transform.SetParent(bote.transform); // Hace que el jugador sea hijo del bote
                    break;
                }
            }
        }
    }

    private void MoverBoteConJugador()
    {
        // Movimiento del bote (el jugador se mueve con el bote)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Movimiento del bote con Rigidbody
        Vector3 movimiento = new Vector3(horizontal, 0f, vertical) * velocidadBote * Time.deltaTime;
        rbBote.MovePosition(rbBote.position + movimiento);

        // Si se desea rotar el bote, agregar rotación
        float rotacion = Input.GetAxis("Horizontal") * velocidadBote;
        rbBote.MoveRotation(Quaternion.Euler(0f, rbBote.rotation.eulerAngles.y + rotacion, 0f));
    }

    // Función de interacción si el jugador sale del bote
    public void SalirDelBote()
    {
        if (enBote && boteSeleccionado != null)
        {
            jugador.transform.SetParent(null); // El jugador deja de ser hijo del bote
            enBote = false;
            boteSeleccionado = null;
        }
    }
}
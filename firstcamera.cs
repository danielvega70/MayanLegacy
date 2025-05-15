using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCamera : MonoBehaviour
{
    [Header("Player Settings")]
    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    public float jumpForce = 7f;

    [Header("Look Settings")]
    public Transform playerCamera;
    public float lookSensitivity = 2f;
    public float maxLookAngle = 80f;

    private Rigidbody rb;
    private bool isGrounded = true;
    private float rotationX = 0f;

    private playercontroller thirdPersonController;

    [Header("Cameras")]
    public Camera mainCamera; // La c�mara principal (primera persona)
    public Camera thirdPersonCamera; // La c�mara en tercera persona

    private void Awake()
    {
        // Obtener el Rigidbody y asegurarse de que est� presente
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody no encontrado en el jugador.");
        }

        // Verificar si la c�mara del jugador est� asignada
        if (!playerCamera)
        {
            Debug.LogError("Player Camera no asignada.");
        }

        // Encontrar el controlador de la c�mara en tercera persona
        thirdPersonController = FindObjectOfType<playercontroller>();
        if (thirdPersonController == null)
        {
            Debug.LogError("No se encontr� PlayerController en la escena.");
        }

        // Aseg�rate de que la c�mara en tercera persona est� desactivada inicialmente.
        if (thirdPersonCamera != null)
        {
            thirdPersonCamera.gameObject.SetActive(false);
        }

        // Bloquear el cursor para no interferir con la visi�n en primera persona
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleLook();
        HandleJump();
        HandleCameraSwitch();

        // Verificar colisiones de la c�mara
        DebugCameraCollision();
        // Verificar si el jugador est� en el suelo
        DebugGroundCheck();
    }

    private void HandleMovement()
    {
        // Obtener la entrada de movimiento horizontal y vertical
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = (transform.right * horizontal + transform.forward * vertical).normalized;

        // Determinar la velocidad seg�n si el jugador est� corriendo o caminando
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        // Mover al jugador
        rb.velocity = new Vector3(direction.x * speed, rb.velocity.y, direction.z * speed);
    }

    private void HandleLook()
    {
        // Obtener los movimientos del rat�n
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        // Controlar la rotaci�n vertical de la c�mara
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -maxLookAngle, maxLookAngle);

        // Aplicar la rotaci�n en el eje X de la c�mara
        if (playerCamera)
        {
            playerCamera.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        }

        // Rotar el jugador horizontalmente
        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleJump()
    {
        // Verificar si el jugador est� en el suelo antes de permitir saltar
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // M�todo adicional para verificar si el jugador est� tocando el suelo utilizando un Raycast
    private void DebugGroundCheck()
    {
        RaycastHit hit;
        // Raycast hacia abajo desde el jugador para verificar si est� tocando el suelo
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
        {
            if (!isGrounded)
            {
                Debug.Log("El jugador est� en el suelo.");
                isGrounded = true; // Actualizamos el estado de suelo
            }
        }
        else
        {
            if (isGrounded)
            {
                Debug.Log("El jugador NO est� en el suelo.");
                isGrounded = false; // Actualizamos el estado de suelo
            }
        }
    }

    private void HandleCameraSwitch()
    {
        // Cambiar entre la c�mara en primera y tercera persona cuando se presiona la tecla "P"
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Alternar entre las dos c�maras
            if (mainCamera != null && thirdPersonCamera != null)
            {
                bool isMainCameraActive = mainCamera.gameObject.activeSelf;
                mainCamera.gameObject.SetActive(!isMainCameraActive);
                thirdPersonCamera.gameObject.SetActive(isMainCameraActive);

                Debug.Log("C�mara cambiada a: " + (isMainCameraActive ? "Tercera persona" : "Primera persona"));
            }
        }
    }

    private void DebugCameraCollision()
    {
        if (playerCamera)
        {
            // Raycast para verificar si la c�mara est� chocando con algo
            RaycastHit hit;
            Ray ray = new Ray(playerCamera.position, playerCamera.forward);

            if (Physics.Raycast(ray, out hit, 2f)) // 2f es el rango de la colisi�n
            {
                Debug.Log("Colisi�n detectada con: " + hit.collider.name);
                // Aqu� puedes implementar l�gica para ajustar la posici�n de la c�mara o tomar acci�n
            }
        }
    }
}

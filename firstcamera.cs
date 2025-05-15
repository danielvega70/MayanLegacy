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
    public Camera mainCamera; // La cámara principal (primera persona)
    public Camera thirdPersonCamera; // La cámara en tercera persona

    private void Awake()
    {
        // Obtener el Rigidbody y asegurarse de que esté presente
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody no encontrado en el jugador.");
        }

        // Verificar si la cámara del jugador está asignada
        if (!playerCamera)
        {
            Debug.LogError("Player Camera no asignada.");
        }

        // Encontrar el controlador de la cámara en tercera persona
        thirdPersonController = FindObjectOfType<playercontroller>();
        if (thirdPersonController == null)
        {
            Debug.LogError("No se encontró PlayerController en la escena.");
        }

        // Asegúrate de que la cámara en tercera persona esté desactivada inicialmente.
        if (thirdPersonCamera != null)
        {
            thirdPersonCamera.gameObject.SetActive(false);
        }

        // Bloquear el cursor para no interferir con la visión en primera persona
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleLook();
        HandleJump();
        HandleCameraSwitch();

        // Verificar colisiones de la cámara
        DebugCameraCollision();
        // Verificar si el jugador está en el suelo
        DebugGroundCheck();
    }

    private void HandleMovement()
    {
        // Obtener la entrada de movimiento horizontal y vertical
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = (transform.right * horizontal + transform.forward * vertical).normalized;

        // Determinar la velocidad según si el jugador está corriendo o caminando
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        // Mover al jugador
        rb.velocity = new Vector3(direction.x * speed, rb.velocity.y, direction.z * speed);
    }

    private void HandleLook()
    {
        // Obtener los movimientos del ratón
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        // Controlar la rotación vertical de la cámara
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -maxLookAngle, maxLookAngle);

        // Aplicar la rotación en el eje X de la cámara
        if (playerCamera)
        {
            playerCamera.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        }

        // Rotar el jugador horizontalmente
        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleJump()
    {
        // Verificar si el jugador está en el suelo antes de permitir saltar
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // Método adicional para verificar si el jugador está tocando el suelo utilizando un Raycast
    private void DebugGroundCheck()
    {
        RaycastHit hit;
        // Raycast hacia abajo desde el jugador para verificar si está tocando el suelo
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
        {
            if (!isGrounded)
            {
                Debug.Log("El jugador está en el suelo.");
                isGrounded = true; // Actualizamos el estado de suelo
            }
        }
        else
        {
            if (isGrounded)
            {
                Debug.Log("El jugador NO está en el suelo.");
                isGrounded = false; // Actualizamos el estado de suelo
            }
        }
    }

    private void HandleCameraSwitch()
    {
        // Cambiar entre la cámara en primera y tercera persona cuando se presiona la tecla "P"
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Alternar entre las dos cámaras
            if (mainCamera != null && thirdPersonCamera != null)
            {
                bool isMainCameraActive = mainCamera.gameObject.activeSelf;
                mainCamera.gameObject.SetActive(!isMainCameraActive);
                thirdPersonCamera.gameObject.SetActive(isMainCameraActive);

                Debug.Log("Cámara cambiada a: " + (isMainCameraActive ? "Tercera persona" : "Primera persona"));
            }
        }
    }

    private void DebugCameraCollision()
    {
        if (playerCamera)
        {
            // Raycast para verificar si la cámara está chocando con algo
            RaycastHit hit;
            Ray ray = new Ray(playerCamera.position, playerCamera.forward);

            if (Physics.Raycast(ray, out hit, 2f)) // 2f es el rango de la colisión
            {
                Debug.Log("Colisión detectada con: " + hit.collider.name);
                // Aquí puedes implementar lógica para ajustar la posición de la cámara o tomar acción
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateMachine : StateMachineBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 2f;    // Velocidad de caminar
    public float runSpeed = 5f;     // Velocidad de correr

    private Rigidbody rb;           // Referencia al Rigidbody para mover al jugador
    private Transform playerTransform; // Transform del jugador
    private bool isRunning = false;   // Flag para saber si el jugador está corriendo
    private bool isWalking = false;   // Flag para saber si el jugador está caminando

    // Este método se llama cuando entramos en el estado de animación
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Obtenemos el Rigidbody del jugador si aún no está asignado
        if (rb == null)
        {
            rb = animator.GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.LogError("Rigidbody no encontrado en el personaje.");
            }
            else
            {
                rb.freezeRotation = true; // Evitar rotación no deseada
            }
        }

        // Obtenemos el Transform del jugador si aún no está asignado
        if (playerTransform == null)
            playerTransform = animator.transform;
    }

    // Este método se llama mientras el estado de animación está activo
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Determinamos la dirección del movimiento
        Vector3 movementDirection = Vector3.zero;

        // Si el jugador está presionando las teclas de movimiento
        if (Input.GetKey(KeyCode.W)) movementDirection.z = 1f;
        if (Input.GetKey(KeyCode.S)) movementDirection.z = -1f;
        if (Input.GetKey(KeyCode.A)) movementDirection.x = -1f;
        if (Input.GetKey(KeyCode.D)) movementDirection.x = 1f;

        movementDirection.Normalize(); // Asegurarse de que la dirección esté normalizada

        // Determinar si el jugador está caminando o corriendo
        isRunning = Input.GetKey(KeyCode.LeftShift);
        isWalking = !isRunning && movementDirection.magnitude > 0.1f;  // Solo caminar si no se está corriendo

        // Si está corriendo, se aplica la velocidad de correr, si no, la velocidad de caminar
        float speed = isRunning ? runSpeed : walkSpeed;

        // Actualizamos la posición del personaje utilizando Rigidbody
        if (rb != null && movementDirection.magnitude >= 0.1f)
        {
            Vector3 movement = playerTransform.TransformDirection(movementDirection) * speed * Time.deltaTime;
            rb.MovePosition(playerTransform.position + movement);
        }

        // Actualizar las animaciones
        animator.SetBool("isWalking", isWalking); // Si está caminando, activa la animación de caminar
        animator.SetBool("isRunning", isRunning); // Si está corriendo, activa la animación de correr
    }

    // Este método se llama cuando salimos del estado de animación
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Aseguramos que las animaciones se desactiven cuando se salga de este estado
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
    }
}
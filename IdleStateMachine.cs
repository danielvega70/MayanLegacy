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
    private bool isRunning = false;   // Flag para saber si el jugador est� corriendo
    private bool isWalking = false;   // Flag para saber si el jugador est� caminando

    // Este m�todo se llama cuando entramos en el estado de animaci�n
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Obtenemos el Rigidbody del jugador si a�n no est� asignado
        if (rb == null)
        {
            rb = animator.GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.LogError("Rigidbody no encontrado en el personaje.");
            }
            else
            {
                rb.freezeRotation = true; // Evitar rotaci�n no deseada
            }
        }

        // Obtenemos el Transform del jugador si a�n no est� asignado
        if (playerTransform == null)
            playerTransform = animator.transform;
    }

    // Este m�todo se llama mientras el estado de animaci�n est� activo
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Determinamos la direcci�n del movimiento
        Vector3 movementDirection = Vector3.zero;

        // Si el jugador est� presionando las teclas de movimiento
        if (Input.GetKey(KeyCode.W)) movementDirection.z = 1f;
        if (Input.GetKey(KeyCode.S)) movementDirection.z = -1f;
        if (Input.GetKey(KeyCode.A)) movementDirection.x = -1f;
        if (Input.GetKey(KeyCode.D)) movementDirection.x = 1f;

        movementDirection.Normalize(); // Asegurarse de que la direcci�n est� normalizada

        // Determinar si el jugador est� caminando o corriendo
        isRunning = Input.GetKey(KeyCode.LeftShift);
        isWalking = !isRunning && movementDirection.magnitude > 0.1f;  // Solo caminar si no se est� corriendo

        // Si est� corriendo, se aplica la velocidad de correr, si no, la velocidad de caminar
        float speed = isRunning ? runSpeed : walkSpeed;

        // Actualizamos la posici�n del personaje utilizando Rigidbody
        if (rb != null && movementDirection.magnitude >= 0.1f)
        {
            Vector3 movement = playerTransform.TransformDirection(movementDirection) * speed * Time.deltaTime;
            rb.MovePosition(playerTransform.position + movement);
        }

        // Actualizar las animaciones
        animator.SetBool("isWalking", isWalking); // Si est� caminando, activa la animaci�n de caminar
        animator.SetBool("isRunning", isRunning); // Si est� corriendo, activa la animaci�n de correr
    }

    // Este m�todo se llama cuando salimos del estado de animaci�n
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Aseguramos que las animaciones se desactiven cuando se salga de este estado
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
    }
}
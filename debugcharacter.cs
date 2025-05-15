using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugcharacter : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private CharacterController characterController;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Depuración del Animator
        if (animator != null)
        {
            Debug.Log($"Animator activo: {animator.isActiveAndEnabled}");
            Debug.Log($"Parámetros en Animator: {animator.parameterCount}");

            foreach (AnimatorControllerParameter param in animator.parameters)
            {
                if (param.type == AnimatorControllerParameterType.Float)
                    Debug.Log($"Parámetro Float: {param.name} - Valor: {animator.GetFloat(param.name)}");
                else if (param.type == AnimatorControllerParameterType.Int)
                    Debug.Log($"Parámetro Int: {param.name} - Valor: {animator.GetInteger(param.name)}");
                else if (param.type == AnimatorControllerParameterType.Bool)
                    Debug.Log($"Parámetro Bool: {param.name} - Valor: {animator.GetBool(param.name)}");
                else if (param.type == AnimatorControllerParameterType.Trigger)
                    Debug.Log($"Parámetro Trigger: {param.name}");
            }

            Debug.Log($"Animator en estado actual: {animator.GetCurrentAnimatorStateInfo(0).IsName(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name)}");
        }
        else
        {
            Debug.LogError("No se encontró el Animator en el GameObject.");
        }

        // Depuración del Rigidbody
        if (rb != null)
        {
            Debug.Log($"Velocidad Rigidbody: {rb.velocity}");
            Debug.Log($"Uso de gravedad: {rb.useGravity}");
        }
        else
        {
            Debug.LogWarning("El Rigidbody no está presente en este objeto.");
        }

        // Depuración del CharacterController (si se usa)
        if (characterController != null)
        {
            Debug.Log($"Velocidad CharacterController: {characterController.velocity}");
            Debug.Log($"Está en el suelo: {characterController.isGrounded}");
        }
        else
        {
            Debug.LogWarning("El CharacterController no está presente en este objeto.");
        }
    }
}
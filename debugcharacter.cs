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
        // Depuraci�n del Animator
        if (animator != null)
        {
            Debug.Log($"Animator activo: {animator.isActiveAndEnabled}");
            Debug.Log($"Par�metros en Animator: {animator.parameterCount}");

            foreach (AnimatorControllerParameter param in animator.parameters)
            {
                if (param.type == AnimatorControllerParameterType.Float)
                    Debug.Log($"Par�metro Float: {param.name} - Valor: {animator.GetFloat(param.name)}");
                else if (param.type == AnimatorControllerParameterType.Int)
                    Debug.Log($"Par�metro Int: {param.name} - Valor: {animator.GetInteger(param.name)}");
                else if (param.type == AnimatorControllerParameterType.Bool)
                    Debug.Log($"Par�metro Bool: {param.name} - Valor: {animator.GetBool(param.name)}");
                else if (param.type == AnimatorControllerParameterType.Trigger)
                    Debug.Log($"Par�metro Trigger: {param.name}");
            }

            Debug.Log($"Animator en estado actual: {animator.GetCurrentAnimatorStateInfo(0).IsName(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name)}");
        }
        else
        {
            Debug.LogError("No se encontr� el Animator en el GameObject.");
        }

        // Depuraci�n del Rigidbody
        if (rb != null)
        {
            Debug.Log($"Velocidad Rigidbody: {rb.velocity}");
            Debug.Log($"Uso de gravedad: {rb.useGravity}");
        }
        else
        {
            Debug.LogWarning("El Rigidbody no est� presente en este objeto.");
        }

        // Depuraci�n del CharacterController (si se usa)
        if (characterController != null)
        {
            Debug.Log($"Velocidad CharacterController: {characterController.velocity}");
            Debug.Log($"Est� en el suelo: {characterController.isGrounded}");
        }
        else
        {
            Debug.LogWarning("El CharacterController no est� presente en este objeto.");
        }
    }
}
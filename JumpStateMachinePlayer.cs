using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStateMachinePlayer : StateMachineBehaviour
{
    private string previousState = "Idle";  // El estado anterior del jugador

    // Este método se llama cuando el estado de la animación comienza.
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Guardamos el estado anterior antes de entrar al salto
        if (animator.GetBool("isWalking"))
        {
            previousState = "Walking";
        }
        else if (animator.GetBool("isRunning"))
        {
            previousState = "Running";
        }
        else
        {
            previousState = "Idle";
        }

        // Aquí, opcionalmente, podemos hacer algo cuando el salto comienza (como activar un sonido o efectos visuales)
    }

    // Este método se llama cuando el estado de la animación termina.
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Volvemos al estado anterior después del salto
        animator.SetBool("isWalking", previousState == "Walking");
        animator.SetBool("isRunning", previousState == "Running");
        animator.SetBool("isIdle", previousState == "Idle");
    }
}
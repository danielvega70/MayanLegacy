using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickStateMachine : StateMachineBehaviour
{
    // Variable para contar el n�mero de kicks
    private int kickCount = 0;

    // Par�metro para detectar cuando se debe cambiar a SwordAttack
    public string swordAttackTrigger = "SwordAttack";  // Aseg�rate de que el par�metro en el Animator se llame as�

    // Este m�todo se llama cuando entra en el estado de animaci�n (Kick)
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        // Inicializar el contador de kicks si es necesario
        if (kickCount >= 3)
        {
            // Si hemos llegado a 3 kicks, restablecemos el contador
            kickCount = 0;
        }
    }

    // Este m�todo se llama cada frame mientras la animaci�n est� en curso
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        // Verificamos si el jugador hizo clic izquierdo
        if (Input.GetMouseButtonDown(0)) // Clic izquierdo del rat�n
        {
            // Si la animaci�n de Kick ha terminado (completa), avanzamos al siguiente kick
            if (stateInfo.normalizedTime >= 1.0f && kickCount < 3)
            {
                kickCount++;  // Aumentamos el contador de kicks

                // Si hemos hecho 3 kicks, activamos el par�metro de SwordAttack para hacer la transici�n
                if (kickCount == 3)
                {
                    animator.SetTrigger(swordAttackTrigger);  // Activamos el trigger para SwordAttack
                }
            }
        }
    }

    // Este m�todo se llama cuando sale del estado de animaci�n
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        // Si queremos resetear o hacer alguna otra cosa cuando salga del estado de kick
    }
}

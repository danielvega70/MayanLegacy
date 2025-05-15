using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickStateMachine : StateMachineBehaviour
{
    // Variable para contar el número de kicks
    private int kickCount = 0;

    // Parámetro para detectar cuando se debe cambiar a SwordAttack
    public string swordAttackTrigger = "SwordAttack";  // Asegúrate de que el parámetro en el Animator se llame así

    // Este método se llama cuando entra en el estado de animación (Kick)
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

    // Este método se llama cada frame mientras la animación está en curso
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        // Verificamos si el jugador hizo clic izquierdo
        if (Input.GetMouseButtonDown(0)) // Clic izquierdo del ratón
        {
            // Si la animación de Kick ha terminado (completa), avanzamos al siguiente kick
            if (stateInfo.normalizedTime >= 1.0f && kickCount < 3)
            {
                kickCount++;  // Aumentamos el contador de kicks

                // Si hemos hecho 3 kicks, activamos el parámetro de SwordAttack para hacer la transición
                if (kickCount == 3)
                {
                    animator.SetTrigger(swordAttackTrigger);  // Activamos el trigger para SwordAttack
                }
            }
        }
    }

    // Este método se llama cuando sale del estado de animación
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        // Si queremos resetear o hacer alguna otra cosa cuando salga del estado de kick
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggerSword : StateMachineBehaviour
{
    private int clickCount = 0;
    private float comboTimer = 0f;
    public float comboResetTime = 1f; // Tiempo para resetear el combo

    private static readonly int clickParam = Animator.StringToHash("ClickCount");
    private static readonly int swordAttackTrigger = Animator.StringToHash("SwordAttack");
    private static readonly int kickTrigger = Animator.StringToHash("Kick");

    // Se activa cuando entra en el estado de ataque
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        comboTimer = comboResetTime;  // Reinicia el tiempo del combo
        clickCount = 0;  // Reinicia el contador de clics
        animator.SetInteger(clickParam, clickCount);  // Actualiza el parámetro del Animator

        Debug.Log($"[DEBUG] Entrando en estado: {stateInfo.fullPathHash}");
    }

    // Se ejecuta mientras el estado está activo (actualiza el contador de clics y gestiona el combo)
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetMouseButtonDown(0)) // Clic izquierdo para atacar con espada
        {
            clickCount++;
            comboTimer = comboResetTime; // Reinicia el contador de tiempo del combo
            animator.SetInteger(clickParam, clickCount); // Actualiza el parámetro del Animator con el número de clics

            Debug.Log($"[DEBUG] Click {clickCount} registrado.");

            // Condición para el primer ataque del combo
            if (clickCount == 1)
            {
                animator.SetTrigger(swordAttackTrigger); // Activar el primer ataque con espada
                Debug.Log("[DEBUG] Ejecutando primer ataque con espada...");
            }
            // Condición para el segundo ataque del combo
            else if (clickCount == 2)
            {
                animator.SetTrigger(swordAttackTrigger); // Continuar con el siguiente ataque con espada
                Debug.Log("[DEBUG] Continuando con el siguiente ataque...");
            }
            // Condición para el tercer clic que ejecuta una patada
            else if (clickCount == 3)
            {
                animator.SetTrigger(kickTrigger); // Realizar la patada
                Debug.Log("[DEBUG] Ejecutando Kick y reseteando combo...");
                ResetCombo(animator); // Resetea el combo después de la patada
            }
        }

        // Actualizar el temporizador del combo
        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
        }
        else
        {
            // Si el tiempo se agotó, resetea el combo
            ResetCombo(animator);
            Debug.Log("[DEBUG] Combo reseteado por tiempo de inactividad.");
        }
    }

    // Se ejecuta cuando el estado termina
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log($"[DEBUG] Saliendo del estado: {stateInfo.fullPathHash}");
    }

    // Método para resetear el combo
    private void ResetCombo(Animator animator)
    {
        clickCount = 0;
        animator.SetInteger(clickParam, clickCount); // Reinicia el contador de clics
    }
}

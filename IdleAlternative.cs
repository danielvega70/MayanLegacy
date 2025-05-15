using UnityEngine;

public class IdleAlternative : StateMachineBehaviour
{
    private float idleTimer = 0f;

    public float idleSwitchInterval = 20f; // Cambiar Idle cada 20 segundos

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("Idle"))
        {
            // Si estamos en el estado de Idle, empezar a alternar entre Idle1 y Idle2
            idleTimer = 0f;
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("Idle"))
        {
            // Alternar entre Idle 1 y Idle 2 cada 20 segundos
            idleTimer += Time.deltaTime;
            if (idleTimer >= idleSwitchInterval)
            {
                animator.SetTrigger("idleSwitch");
                idleTimer = 0f;
            }
        }
    }
}
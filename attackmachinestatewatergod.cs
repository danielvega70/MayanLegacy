using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class attackmachinestatewatergod : StateMachineBehaviour
{
    // Al entrar en el estado Attack
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<NavMeshAgent>().isStopped = true; // Detener movimiento al atacar
    }

    // Mientras esté en el estado Attack
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform player = animator.GetComponent<enemyAI>().player;

        if (player != null)
        {
            Vector3 direction = (player.position - animator.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, lookRotation, Time.deltaTime * 5f); // Girar hacia el jugador
        }
    }
}
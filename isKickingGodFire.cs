using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickingGodFire : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Enemigo comenz� a atacar (Patada)");
        // Aqu� puedes activar efectos o sonidos del ataque
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // L�gica de ataque continuo si es necesario
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Enemigo termin� su ataque");
    }
}
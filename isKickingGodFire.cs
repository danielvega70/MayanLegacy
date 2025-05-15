using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickingGodFire : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Enemigo comenzó a atacar (Patada)");
        // Aquí puedes activar efectos o sonidos del ataque
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Lógica de ataque continuo si es necesario
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Enemigo terminó su ataque");
    }
}
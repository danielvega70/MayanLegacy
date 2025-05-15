using UnityEngine;

public class SwordFightingStateMachine : StateMachineBehaviour
{
    private int clickCount = 0;  // Contador de clics
    private float comboTimer = 0f; // Temporizador para reiniciar los clics
    public float comboResetTime = 1f;  // Tiempo para reiniciar el combo

    private static readonly int clickParam = Animator.StringToHash("ClickCount");
    private static readonly int swordAttackTrigger = Animator.StringToHash("SwordAttack");
    private static readonly int kickTrigger = Animator.StringToHash("Kick");

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        comboTimer = comboResetTime; // Reiniciar temporizador
        clickCount = 0; // Reiniciar clics
        SetAnimatorInteger(animator, clickParam, clickCount);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetMouseButtonDown(1))  // Si se hace clic derecho
        {
            clickCount++;  // Incrementar contador
            comboTimer = comboResetTime; // Reiniciar temporizador
            SetAnimatorInteger(animator, clickParam, clickCount);

            if (clickCount == 1)
            {
                // Primer ataque con espada
                animator.SetTrigger(swordAttackTrigger);
                Debug.Log("Primer ataque con espada.");
            }
            else if (clickCount == 2)
            {
                // Segundo ataque (otro golpe de espada)
                animator.SetTrigger(swordAttackTrigger);
                Debug.Log("Segundo ataque con espada.");
            }
            else if (clickCount >= 3)
            {
                // Tercer clic o más -> Ejecuta la patada y reinicia el combo
                animator.SetTrigger(kickTrigger);
                Debug.Log("Tercer clic - Patada.");
                ResetCombo(animator);
            }
        }

        // Reiniciar el combo si el tiempo de espera se acaba
        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
        }
        else
        {
            ResetCombo(animator);
        }
    }

    private void ResetCombo(Animator animator)
    {
        clickCount = 0;
        SetAnimatorInteger(animator, clickParam, 0);
        comboTimer = comboResetTime;
    }

    private void SetAnimatorInteger(Animator animator, int parameterHash, int value)
    {
        if (animator.HasParameter(parameterHash))
        {
            animator.SetInteger(parameterHash, value);
        }
    }
}

public static class AnimatorExtensions
{
    public static bool HasParameter(this Animator animator, int parameterHash)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.nameHash == parameterHash)
                return true;
        }
        return false;
    }
}

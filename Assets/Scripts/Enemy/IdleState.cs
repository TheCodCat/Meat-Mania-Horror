using UnityEngine;

public class IdleState : StateMachineBehaviour
{
    private Enemy _myEnemy;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.transform.parent.TryGetComponent(out _myEnemy))
        {
            Debug.Log("Нашли себя");
        }
    }

// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Physics.CheckSphere(_myEnemy.transform.position, _myEnemy.EnemyParametrs.MinRadius, _myEnemy.LayerMask))
        {
            Debug.Log("Атака");
            animator.SetTrigger("Attack");
        }
        if (Physics.CheckSphere(_myEnemy.transform.position, _myEnemy.EnemyParametrs.MaxRadius, _myEnemy.LayerMask))
        {
            Debug.Log("Игрок в поле зрения");
            animator.SetBool("IsTarget", true);
        }
    }
}

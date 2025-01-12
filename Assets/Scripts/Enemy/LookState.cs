using UnityEngine;

public class LookState : StateMachineBehaviour
{
    private Enemy _myEnemy;
    private Ray _ray;
    bool _playerCan;
    bool _isMin;
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
        _playerCan = _myEnemy.FieldOfView.RayToScan();
        Debug.Log(_playerCan);
        _isMin = Physics.CheckSphere(_myEnemy.transform.position, _myEnemy.EnemyParametrs.MinRadius, _myEnemy.LayerMask);
        if (_isMin) animator.SetTrigger(Enemy.ATTACK_KEY);
        if (_playerCan)
        {
            animator.SetBool(Enemy.WALK_KEY, true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(Enemy.LOOK_KEY, false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

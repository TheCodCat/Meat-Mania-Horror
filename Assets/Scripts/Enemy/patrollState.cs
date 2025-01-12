using UnityEngine;

public class patrollState : StateMachineBehaviour
{
    private Enemy _myEnemy;
    private int _pointIndex;
    bool _playerCan;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.transform.parent.TryGetComponent(out _myEnemy))
        {
            Debug.Log("Нашли себя");
            _myEnemy.Agent.autoBraking = false;
            _myEnemy.Agent.stoppingDistance = 0;
            _pointIndex++;
            _pointIndex %= _myEnemy.WPF.Length;

            _myEnemy.Agent.SetDestination(_myEnemy.WPF[_pointIndex].position);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerCan = _myEnemy.FieldOfView.RayToScan();
        if (_playerCan) animator.SetTrigger(Enemy.WALK_KEY);

        if (_myEnemy.Agent.remainingDistance <= 0.5)
        {
            animator.SetBool(Enemy.LOOK_KEY, true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _myEnemy.Agent.SetDestination(_myEnemy.transform.position);
        _myEnemy.Agent.autoBraking = true;
        _myEnemy.Agent.stoppingDistance = _myEnemy.EnemyParametrs.DistanseStopping;
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

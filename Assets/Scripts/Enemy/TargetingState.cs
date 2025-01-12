using UnityEngine;

public class TargetingState : StateMachineBehaviour
{
    private Enemy _myEnemy;
    bool _isMin;
    bool _isCanPlayer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.transform.parent.TryGetComponent(out _myEnemy))
        {
            Debug.Log("Все ок");
        }
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        _myEnemy.Agent.SetDestination(_myEnemy.Player.transform.position);

        _isCanPlayer = _myEnemy.FieldOfView.RayToScan();
        _isMin = Physics.CheckSphere(_myEnemy.transform.position, _myEnemy.EnemyParametrs.MinRadius, _myEnemy.LayerMask);
        if (_isMin)
        {
            animator.SetTrigger("Attack");
        }
        
        if(!_isMin && !_isCanPlayer)
        {
            animator.SetTrigger(Enemy.LOOK_KEY);
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(Enemy.WALK_KEY ,false);
        _myEnemy.Agent.SetDestination(_myEnemy.transform.position);
    }
}

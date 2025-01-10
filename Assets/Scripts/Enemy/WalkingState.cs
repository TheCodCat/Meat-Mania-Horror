using UnityEngine;

public class WalkingState : StateMachineBehaviour
{
    private Enemy _myEnemy;
    bool _isMin;
    bool _isMax;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.transform.parent.TryGetComponent(out _myEnemy))
        {
            Debug.Log("Нашли себя");
        }
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _isMin = Physics.CheckSphere(_myEnemy.transform.position, _myEnemy.EnemyParametrs.MinRadius, _myEnemy.LayerMask);
        _isMax = Physics.CheckSphere(_myEnemy.transform.position, _myEnemy.EnemyParametrs.MaxRadius, _myEnemy.LayerMask);

        if (_isMax)
        {
            Debug.Log("Игрок в поле зрения");
            animator.SetBool("IsTarget", true);
            _myEnemy.Agent.SetDestination(_myEnemy.Player.transform.position);
        }
        else
        {
            _myEnemy.Agent.SetDestination(_myEnemy.transform.position);
            animator.SetBool("IsTarget", false);
        }
        if(_isMax && _isMin)
        {
            animator.SetTrigger("Attack");
        }
    }
}

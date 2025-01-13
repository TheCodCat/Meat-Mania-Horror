using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class ChaseState: State
    {
        private EnemyAI _enemyAI;

        public ChaseState(EnemyAI enemyAI)
        {
            _enemyAI = enemyAI;
        }
        public override void StartState()
        {
            _enemyAI.Animator.SetTrigger(EnemyAI.CHASE_KEY);
            _enemyAI.AI_Enemy = AI_State.Chase;
            _enemyAI.AI_Agent.stoppingDistance = 1.2f;
        }
        public override void UpdateState()
        {
            if (_enemyAI.FieldOfView.FieldOfViewCheck() && !_enemyAI.Check_LastPoint)
            {
                _enemyAI.AI_Agent.SetDestination(_enemyAI.Player.transform.position);
                _enemyAI.Last_point = new Vector3(_enemyAI.Player.transform.position.x, _enemyAI.Player.transform.position.y, _enemyAI.Player.transform.position.z);
            }
            else
            {
                _enemyAI.AI_Agent.stoppingDistance = 0;
                _enemyAI.Check_LastPoint = true;

                _enemyAI.AI_Agent.SetDestination(_enemyAI.Last_point);

                if(_enemyAI.AI_Agent.remainingDistance < 1)
                    _enemyAI.StateMachine.ChangeState(_enemyAI.LookState);
            }
        }
        public override void EndState()
        {
            _enemyAI.AI_Agent.stoppingDistance = 1.2f;
        }
    }
}

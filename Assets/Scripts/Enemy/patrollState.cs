using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class PatrollState : State
    {
        private EnemyAI _enemyAI;

        public PatrollState(EnemyAI enemyAI)
        {
            _enemyAI = enemyAI;
        }
        public override void StartState()
        {
            _enemyAI.AI_Agent.stoppingDistance = 0;
            _enemyAI.AI_Enemy = AI_State.Patrol;
            _enemyAI.Animator.SetTrigger(EnemyAI.PATROLL_KEY);
        }

        public override void UpdateState()
        {
            if (_enemyAI.FieldOfView.FieldOfViewCheck())
            {
                Debug.Log("Логика приследования");
                _enemyAI.StateMachine.ChangeState(_enemyAI.ChaseState);
            }
            else
            {
                _enemyAI.AI_Agent.SetDestination(_enemyAI.WayPoints[_enemyAI.Current_Patch].transform.position);
                if (_enemyAI.AI_Agent.remainingDistance <= 1)
                {
                    _enemyAI.Current_Patch++;
                    _enemyAI.Current_Patch = _enemyAI.Current_Patch % _enemyAI.WayPoints.Length;
                }
            }

        }

        public override void EndState()
        {
            Debug.Log("Остановочка");
            _enemyAI.AI_Agent.SetDestination(_enemyAI.AI_Agent.transform.position);
        }
    }
}

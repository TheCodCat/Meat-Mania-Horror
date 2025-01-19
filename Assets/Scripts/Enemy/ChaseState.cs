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
            _enemyAI.AudioSource.clip = _enemyAI.AudioClipRunning;
            _enemyAI.AudioSource.Play();
            _enemyAI.AI_Agent.speed = EnemyAI.RunningSpeed;
            _enemyAI.Animator.SetTrigger(EnemyAI.CHASE_KEY);
            _enemyAI.AI_Enemy = AI_State.Chase;
            _enemyAI.AI_Agent.stoppingDistance = 1.2f;
            _enemyAI.Last_point = new Vector3(_enemyAI.Player.transform.position.x, _enemyAI.Player.transform.position.y, _enemyAI.Player.transform.position.z);
        }
        public override void UpdateState()
        {
            if(_enemyAI.FieldOfView.FieldOfViewCheck())
            {
                _enemyAI.AI_Agent.SetDestination(_enemyAI.Player.transform.position);
                Debug.Log(_enemyAI.AI_Agent.remainingDistance);
                //if (_enemyAI.AI_Agent.remainingDistance <= 2)
                //{
                //    _enemyAI.StateMachine.ChangeState(_enemyAI.AttackEnemy);
                //}
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
            _enemyAI.AudioSource.Stop();
            _enemyAI.AI_Agent.SetDestination(_enemyAI.AI_Agent.transform.position);
            _enemyAI.AI_Agent.stoppingDistance = 1.2f;
        }
    }
}

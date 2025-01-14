using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class LookState : State
    {
        private EnemyAI _enemyAI;
        public LookState(EnemyAI enemyAI) 
        {
            _enemyAI = enemyAI;
        }

        public override async void StartState()
        {
            _enemyAI.AI_Enemy = AI_State.Stay;

            _enemyAI.Animator.SetTrigger(EnemyAI.LOOK_KEY);

            await UniTask.Delay(TimeSpan.FromSeconds(UnityEngine.Random.Range(2, 4)));

            if (_enemyAI.FieldOfView.FieldOfViewCheck())
            {
                Debug.Log("Приследование");
                _enemyAI.StateMachine.ChangeState(_enemyAI.ChaseState);
            }
            else
            {
                _enemyAI.StateMachine.ChangeState(_enemyAI.PatrollState);
            }
        }
    }
}

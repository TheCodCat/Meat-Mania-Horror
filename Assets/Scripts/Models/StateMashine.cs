namespace Assets.Scripts.Models
{
    public class StateMashine
    {
        public State CurrentState { get;private set; }

        public void UpdateState()
        {
            CurrentState.UpdateState();
        }
        public void ChangeState(State state)
        {
            CurrentState?.EndState();
            CurrentState = state;
            CurrentState?.StartState();
        }
    }
}

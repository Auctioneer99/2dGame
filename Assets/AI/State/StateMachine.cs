namespace Assets.AI.State
{
    public class StateMachine<T> : IStateSwitcher<T> where T : IStateModel  //StateFactory<T> 
    {
        public IState<T> CurrentState { get; private set; }

        private T _stateModel;

        public StateMachine(T stateModel)
        {
            _stateModel = stateModel;
        }

        public void ChangeState<S>() where S : IState<T>, new()
        {
            var state = getState<S>();
            if (CurrentState != null)
            {
                CurrentState.Exit(state);
            }
            var temp = CurrentState;
            CurrentState = state;
            if (temp != null)
            {
                CurrentState.Enter(temp);
            }
        }

        private S getState<S>() where S : IState<T>, new()
        {
            var state = new S();
            state.Initialize(this, _stateModel);
            return state;
        }
    }
    /*
    public interface StateFactory<T> where T : IStateModel
    {
        S getState<S>() where S : IState<T>, new();
    }
    */
}

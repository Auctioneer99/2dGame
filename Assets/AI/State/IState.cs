namespace Assets.AI.State
{
    public interface IState<T> where T : IStateModel
    {
        public void Initialize(IStateSwitcher<T> changer, T model);
        public void Update(float deltaTime);
        public void Enter(IState<T> last);
        public void Exit(IState<T> next);
    }
}

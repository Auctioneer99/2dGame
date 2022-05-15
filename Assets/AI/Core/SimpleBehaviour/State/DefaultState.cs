using Assets.AI.State;
using UnityEngine;

namespace Assets.AI.Core.SimpleBehaviour.State
{
    public class DefaultState : IState<AIStateModel>
    {
        private AIStateModel _model;
        private IStateSwitcher<AIStateModel> _changer;

        public void Initialize(IStateSwitcher<AIStateModel> changer, AIStateModel model)
        {
            _changer = changer;
            _model = model;
        }

        public void Enter(IState<AIStateModel> last)
        {

        }

        public void Exit(IState<AIStateModel> next)
        {

        }

        public void Update(float deltaTime)
        {
            if (Input.GetKey(KeyCode.V))
            {
                _changer.ChangeState<WriterState>();
            }
            if (Input.GetKey(KeyCode.C))
            {
                _changer.ChangeState<IdleState>();
            }
        }
    }
}

using Assets.AI.State;
using UnityEngine;

namespace Assets.AI.Core.SimpleBehaviour.State
{
    public class WriterState : IState<AIStateModel>
    {
        private IStateSwitcher<AIStateModel> _changer;
        private AIStateModel _model;

        public void Initialize(IStateSwitcher<AIStateModel> changer, AIStateModel model)
        {
            _changer = changer;
            _model = model;
        }

        public void Enter(IState<AIStateModel> last)
        {
            Debug.Log("Entered");
        }

        public void Exit(IState<AIStateModel> next)
        {
            Debug.Log("Exited");
        }

        public void Update()
        {
            if (Input.GetKey(KeyCode.V))
            {
                _changer.ChangeState<DefaultState>();
            }
        }
    }
}

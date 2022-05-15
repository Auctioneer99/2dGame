using Assets.AI.State;
using UnityEngine;

namespace Assets.AI.Core.SimpleBehaviour.State
{
    public class IdleState : IState<AIStateModel>
    {
        private const float ACHIEVE_DISTANCE = 1f;

        private AIStateModel _model;
        private IStateSwitcher<AIStateModel> _changer;

        public void Initialize(IStateSwitcher<AIStateModel> changer, AIStateModel model)
        {
            _changer = changer;
            _model = model;
        }

        public void Update(float deltaTime)
        {
            var aim = _model.AimProvider.getAim();
            if (aim != null)
            {
                if (Vector3.Distance(aim.Position, _model.PlayerController.transform.position) > ACHIEVE_DISTANCE)
                {
                    _changer.ChangeState<WalkingState>();
                }
            }
        }

        public void Enter(IState<AIStateModel> last)
        {
            _model.PlayerController.SetInputs(0, 0, -0, 0);
            Debug.Log("Жду приказаний");
        }

        public void Exit(IState<AIStateModel> next)
        {

        }
    }
}

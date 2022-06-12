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
            if (!_model.PlayerController.dead)
            {
                var aim = _model.AimProvider.getAim();
                if (aim != null)
                {
                    if (Vector3.Distance(aim.Position, _model.PlayerController.transform.position) > ACHIEVE_DISTANCE)
                    {
                        _changer.ChangeState<WalkingState>();
                    }
                }

                if (_model.PlayerDetection.IsPlayerVisible(_model.PlayerController.transform.position))
                {
                    _changer.ChangeState<BattleState>();
                }
            }
        }

        public void Enter(IState<AIStateModel> last)
        {
            if (!_model.PlayerController.dead)
            {
                _model.PlayerController.SetInputs(0, 0, -0, 0);
            }
            _model.PlayerController.HandleWalking(false, false);
            Debug.Log("Жду приказаний");
        }

        public void Exit(IState<AIStateModel> next)
        {

        }
    }
}

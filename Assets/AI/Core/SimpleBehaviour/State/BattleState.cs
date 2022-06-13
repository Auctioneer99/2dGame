using Assets.AI.Detection.Player;
using Assets.AI.State;
using UnityEngine;

namespace Assets.AI.Core.SimpleBehaviour.State
{
    public class BattleState : IState<AIStateModel>
    {
        private AIStateModel _model;
        private IStateSwitcher<AIStateModel> _changer;
        private IPlayerDetection _detection;

        private Vector3 _lastSeenLocation;

        public void Initialize(IStateSwitcher<AIStateModel> changer, AIStateModel model)
        {
            _changer = changer;
            _model = model;

            _detection = _model.PlayerDetection;
        }

        public void Update(float deltaTime)
        {
            if (!_model.PlayerController.dead)
            {
                var player = _detection.Player;
                if (_detection.IsPlayerVisible(_model.PlayerController.transform.position))
                {
                    _lastSeenLocation = player.transform.position;
                }
                else
                {
                    _model.AddPrimaryAim(_lastSeenLocation);
                    _changer.ChangeState<WalkingState>();
                    return;
                }

                _model.ShootExcecutor.Shoot(player.transform.position - _model.PlayerController.transform.GetChild(2).GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(5).position);
                //Shoot;
            }
        }

        public void Enter(IState<AIStateModel> last)
        {
            _lastSeenLocation = _detection.Player.transform.position;
        }

        public void Exit(IState<AIStateModel> next)
        {

        }
    }
}

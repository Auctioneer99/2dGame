using Assets.AI.Detection.Aim;
using Assets.AI.Detection.NavMesh;
using Assets.AI.Detection.Player;
using Assets.AI.State;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.AI.Core.SimpleBehaviour.State
{
    public class WalkingState : IState<AIStateModel>
    {
        private const float ACHIVE_DISTANCE = 1f;

        private AIStateModel _model;
        private IStateSwitcher<AIStateModel> _changer;

        private NavMeshAgent _agent;
        private IAim _aim;

        public void Initialize(IStateSwitcher<AIStateModel> changer, AIStateModel model)
        {
            _changer = changer;
            _model = model;
            _agent = model.NavigationAgent;
        }

        public void Update(float deltaTime)
        {
            if (_agent.destination != _aim.Position)
            {
                _agent.SetDestination(_aim.Position);
            }

            if (_agent.isOnOffMeshLink)
            {
                var link = (OffMeshLink)_agent.navMeshOwner;
                var handler = link.GetComponent<NavMeshLinkHandler>();
                handler.Accept(_changer);
                return;
            }

            _model.PlayerController.Move(_agent.velocity);

            var dist = Vector3.Distance(_aim.Position, _model.PlayerController.transform.position);

            if (dist < ACHIVE_DISTANCE)
            {
                _aim.OnComplete();
                _changer.ChangeState<IdleState>();
            }

            if (_model.PlayerDetection.IsPlayerVisible(_model.PlayerController.transform.position))
            {
                _changer.ChangeState<BattleState>();
            }
        }

        public void Enter(IState<AIStateModel> last)
        {
            _model.NavigationAgent.autoTraverseOffMeshLink = false;
            _aim = _model.AimProvider.getAim();
        }

        public void Exit(IState<AIStateModel> next)
        {
            
        }
    }
}

using Assets.AI.Detection.Aim;
using Assets.AI.Detection.NavMesh;
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


            var movingRight = _agent.velocity.x > 0;
            if (movingRight)
            {
                _model.PlayerController.SetInputs(1, 0, 1, 0);
                _model.PlayerController.HandleWalking(false, true);
            }
            var movingLeft = _agent.velocity.x < 0;
            if (movingLeft)
            {
                _model.PlayerController.SetInputs(-1, 0, -1, 0);
                _model.PlayerController.HandleWalking(true, false);
            }
            if ((movingLeft || movingRight) == false)
            {
                _model.PlayerController.SetInputs(0, 0, -0, 0);
            }

            if (Vector3.Distance(_aim.Position, _model.PlayerController.transform.position) < ACHIVE_DISTANCE)
            {
                _aim.OnComplete();
                _changer.ChangeState<IdleState>();
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

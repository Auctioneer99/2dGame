using Assets.AI.State;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.AI.Core.SimpleBehaviour.State
{
    public class JumpState : IState<AIStateModel>
    {
        private const float ACHIEVE_DISTANCE = 1f;
        private const float JUMP_TIME_BEFORE_WALK = 2f;

        private AIStateModel _model;
        private IStateSwitcher<AIStateModel> _changer;

        private NavMeshAgent _agent;
        private Vector3 _aim;
        private bool _jumped = false;
        private float _jumpTimeDelta = 0;

        public void Initialize(IStateSwitcher<AIStateModel> changer, AIStateModel model)
        {
            _changer = changer;
            _model = model;
            _agent = model.NavigationAgent;
        }

        public void Update(float deltaTime)
        {
            if (_jumped)
            {
                if (_model.ColliderChecker.CollidingBootom && _jumpTimeDelta >= JUMP_TIME_BEFORE_WALK)
                {
                    _changer.ChangeState<WalkingState>();
                }
            }
            else
            {
                if (_model.ColliderChecker.CollidingBootom)
                {
                    _model.PlayerController.HandleJumping();
                    _jumped = true;
                }
            }

            _model.PlayerController.Move(_aim - _model.PlayerController.transform.position);

            var distance = Vector3.Distance(_model.PlayerController.transform.position, _aim);//  Math.Abs(_model.PlayerController.transform.position.x - _aim.x);
            if (distance < ACHIEVE_DISTANCE)
            {
                //_agent.CompleteOffMeshLink();
                _changer.ChangeState<WalkingState>();
                return;
            }
            _jumpTimeDelta += deltaTime;
        }

        public void Enter(IState<AIStateModel> last)
        {
            if (_agent.isOnOffMeshLink == false)
            {
                _changer.ChangeState<WalkingState>();
                return;
            }
            var data = _agent.currentOffMeshLinkData;
            var playerPos = _model.PlayerController.transform.position;
            var dist1 = Vector3.Distance(data.startPos, playerPos);
            var dist2 = Vector3.Distance(data.endPos, playerPos);
            _aim = dist1 < dist2 ? data.endPos : data.startPos;
        }

        public void Exit(IState<AIStateModel> next)
        {

        }
    }
}

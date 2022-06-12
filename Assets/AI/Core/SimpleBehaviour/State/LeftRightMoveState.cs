using Assets.AI.State;
using UnityEngine;

namespace Assets.AI.Core.SimpleBehaviour.State
{
    public class LeftRightMoveState : IState<AIStateModel>
    {
        private AIStateModel _model;
        private IStateSwitcher<AIStateModel> _changer;
        private bool isMovingLeft = false;

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

        public void Enter<S>(S last) where S : IState<AIStateModel>
        {
            throw new System.NotImplementedException();
        }

        public void Exit<S>(S next) where S : IState<AIStateModel>
        {
            throw new System.NotImplementedException();
        }

        public void Update(float deltaTime)
        {
            var moved = false;
            if (isMovingLeft == false && _model.ColliderChecker.CollidingRight == false)
            {
                if (_model.ColliderChecker.HasGroundRight)
                {
                    _model.PlayerController.SetInputs(1, 0, 1, 0);
                    _model.PlayerController.HandleWalking(false, true);
                    moved = true;
                }
                else
                {
                    isMovingLeft = true;
                }
            }
            else
            {
                isMovingLeft = true;
            }
            if (isMovingLeft && _model.ColliderChecker.CollidingLeft == false)
            {
                if (_model.ColliderChecker.HasGroundLeft)
                {
                    _model.PlayerController.SetInputs(-1, 0, -1, 0);
                    _model.PlayerController.HandleWalking(true, false);
                    moved = true;
                }
                else
                {
                    isMovingLeft = false;
                }
            }
            else
            {
                isMovingLeft = false;
            }
            if (moved == false)
            {
                _model.PlayerController.SetInputs(0, 0, 0, 0);
            }
        }
    }
}

using Assets.AI.Core.SimpleBehaviour.State;
using Assets.AI.Detection;
using Assets.AI.State;
using UnityEngine;

namespace Assets.AI.Core.SimpleBehaviour
{
    public class AIStateMachine : MonoBehaviour
    {
        private StateMachine<AIStateModel> _stateMachine;

        private AIStateModel _model;

        [SerializeField]
        private ColliderChecker _colliderChecker;
        [SerializeField]
        private PlayerController _playerController;

        private void OnEnable()
        {
            _model = new AIStateModel(_colliderChecker, _playerController);
            _stateMachine = new StateMachine<AIStateModel>(_model);
            _stateMachine.ChangeState<DefaultState>();
        }

        private void Update()
        {
            _stateMachine.CurrentState.Update();
        }
    }
}

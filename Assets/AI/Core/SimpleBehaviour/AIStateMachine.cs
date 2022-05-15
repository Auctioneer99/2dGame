using Assets.AI.Core.SimpleBehaviour.State;
using Assets.AI.Detection;
using Assets.AI.Detection.Aim;
using Assets.AI.State;
using UnityEngine;
using UnityEngine.AI;

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
        [SerializeField]
        private NavMeshAgent _navMeshAgent;
        [SerializeField]
        private AimProvider _aimProvider;


        private void OnEnable()
        {
            _model = new AIStateModel(_colliderChecker, _playerController, _navMeshAgent, _aimProvider);
            _stateMachine = new StateMachine<AIStateModel>(_model);
            _stateMachine.ChangeState<DefaultState>();
        }

        private void Update()
        {
            _stateMachine.CurrentState.Update(Time.deltaTime);
        }
    }
}

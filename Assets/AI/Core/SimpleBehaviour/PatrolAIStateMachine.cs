using Assets.AI.Core.SimpleBehaviour.State;
using Assets.AI.Detection;
using Assets.AI.Detection.Aim;
using Assets.AI.State;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.AI.Core.SimpleBehaviour
{
    public class PatrolAIStateMachine : MonoBehaviour
    {
        private StateMachine<AIStateModel> _stateMachine;

        [SerializeField]
        private List<Transform> _path;
        [SerializeField]
        private ColliderChecker _colliderChecker;
        [SerializeField]
        private EnemyController _playerController;
        [SerializeField]
        private NavMeshAgent _navMeshAgent;

        private void OnEnable()
        {
            var aimProvider = new PatrolAimProvider(_path.Select(t => new PatrolAim(t)).ToArray());
            var model = new AIStateModel(_colliderChecker, _playerController, _navMeshAgent, aimProvider);
            _stateMachine = new StateMachine<AIStateModel>(model);
            _stateMachine.ChangeState<DefaultState>();
        }

        private void Update()
        {
            _stateMachine.CurrentState.Update(Time.deltaTime);
        }
    }
}

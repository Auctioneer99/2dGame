using Assets.AI.Detection;
using Assets.AI.Detection.Aim;
using Assets.AI.Detection.Player;
using Assets.AI.State;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.AI.Core.SimpleBehaviour
{
    public class AIStateModel : IStateModel
    {
        public IColliderChecker ColliderChecker { get; private set; }

        public PlayerController PlayerController { get; private set; }

        public NavMeshAgent NavigationAgent { get; private set; }

        public IPlayerDetection PlayerDetection { get; private set; }

        public IAimProvider AimProvider => _multiAimProvider;

        public IShooting ShootExcecutor { get; private set; }

        private MultiAimProvider _multiAimProvider;

        public AIStateModel(IColliderChecker colliderChecker, PlayerController playerController, NavMeshAgent navMeshAgent, IAimProvider aimProvider, IPlayerDetection playerDetection, IShooting shooting)
        {
            ColliderChecker = colliderChecker;
            PlayerController = playerController;
            NavigationAgent = navMeshAgent;
            PlayerDetection = playerDetection;
            ShootExcecutor = shooting;
            _multiAimProvider = new MultiAimProvider(aimProvider);
        }

        public void AddPrimaryAim(Vector3 position)
        {
            _multiAimProvider.AddPrimaryAim(position);
        }
    }
}

using Assets.AI.Detection;
using Assets.AI.Detection.Aim;
using Assets.AI.State;
using UnityEngine.AI;

namespace Assets.AI.Core.SimpleBehaviour
{
    public class AIStateModel : IStateModel
    {
        public IColliderChecker ColliderChecker { get; private set; }

        public EnemyController PlayerController { get; private set; }

        public NavMeshAgent NavigationAgent { get; private set; }

        public IAimProvider AimProvider { get; private set; }

        public AIStateModel(IColliderChecker colliderChecker, EnemyController playerController, NavMeshAgent navMeshAgent, IAimProvider aimProvider)
        {
            ColliderChecker = colliderChecker;
            PlayerController = playerController;
            NavigationAgent = navMeshAgent;
            AimProvider = aimProvider;
        }
    }
}

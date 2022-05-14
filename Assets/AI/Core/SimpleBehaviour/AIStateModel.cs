using Assets.AI.Detection;
using Assets.AI.State;

namespace Assets.AI.Core.SimpleBehaviour
{
    public class AIStateModel : IStateModel
    {
        public IColliderChecker ColliderChecker { get; private set; }

        public PlayerController PlayerController { get; private set; }

        public AIStateModel(IColliderChecker colliderChecker, PlayerController playerController)
        {
            ColliderChecker = colliderChecker;
            PlayerController = playerController;
        }
    }
}

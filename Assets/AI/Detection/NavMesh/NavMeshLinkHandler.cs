using Assets.AI.Core.SimpleBehaviour;
using Assets.AI.Core.SimpleBehaviour.State;
using Assets.AI.State;
using UnityEngine;

namespace Assets.AI.Detection.NavMesh
{
    public class NavMeshLinkHandler : MonoBehaviour
    {
        public void Accept(IStateSwitcher<AIStateModel> switcher)
        {
            switcher.ChangeState<JumpState>();
        }
    }
}

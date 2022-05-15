using UnityEngine;

namespace Assets.AI.Detection.Aim
{
    public class AimProvider : MonoBehaviour, IAimProvider
    {
        [SerializeField]
        private Transform _location;

        private Aim _aim;

        private void OnEnable()
        {
            _aim = new Aim(_location);
        }

        public IAim getAim()
        {
            return _aim;
        }
    }
}

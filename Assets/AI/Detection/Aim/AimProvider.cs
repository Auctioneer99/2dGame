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

    public interface IAimProvider
    {
        IAim getAim();
    }

    public class Aim : IAim
    {
        public Vector3 Position => _location.position;

        private Transform _location;
        public Aim(Transform transform)
        {
            _location = transform;
        }

        public void OnComplete()
        {
            Debug.Log("Completed");
        }
    }

    public interface IAim
    {
        Vector3 Position { get; }

        public void OnComplete();
    }
}

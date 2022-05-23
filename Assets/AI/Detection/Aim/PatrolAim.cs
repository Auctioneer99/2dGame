using UnityEngine;

namespace Assets.AI.Detection.Aim
{
    public class PatrolAim : IAim
    {
        public bool Achieved { get; private set; }

        public Vector3 Position => _transform.position;

        private Transform _transform;

        public PatrolAim(Transform transform)
        {
            _transform = transform;
        }

        public void OnComplete()
        {
            Achieved = true;
        }

        public void Reset()
        {
            Achieved = false;
        }
    }
}

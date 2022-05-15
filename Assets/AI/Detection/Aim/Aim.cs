using UnityEngine;

namespace Assets.AI.Detection.Aim
{
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
}

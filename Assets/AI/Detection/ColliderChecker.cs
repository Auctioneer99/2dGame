using UnityEngine;

namespace Assets.AI.Detection
{
    public class ColliderChecker : MonoBehaviour, IColliderChecker
    {
        public bool CollidingBootom => _bottomSensor.IsOverlaping;

        public bool CollidingLeft => _leftSensor.IsOverlaping;

        public bool CollidingRight => _rightSensor.IsOverlaping;

        public bool HasGroundLeft => _leftGround.IsOverlaping;

        public bool HasGroundRight => _rightGround.IsOverlaping;

        [SerializeField]
        private Sensor _bottomSensor,
            _leftSensor,
            _rightSensor,
            _leftGround,
            _rightGround;

    }
}

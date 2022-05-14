using UnityEngine;

namespace Assets.AI.Detection
{
    public class Sensor : MonoBehaviour
    {
        public bool IsOverlaping => _collisionCounter > 0;

        private int _collisionCounter;

        private void OnTriggerEnter(Collider other)
        {
            _collisionCounter++;
        }

        private void OnTriggerExit(Collider other)
        {
            _collisionCounter--;
        }
    }
}

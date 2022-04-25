using UnityEngine;

namespace Assets.Scripts
{
    public class SurfaceSlider : MonoBehaviour
    {
        [SerializeField] private float _maxSlopeAngle;

        public bool Grounded { get; private set; }

        private Vector3 _normal;

        public Vector3 Project(Vector3 direction)
        {
            return direction - Vector3.Dot(direction, _normal) * _normal;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var n = collision.GetContact(0).normal;
            var angle = Vector3.Angle(n, Vector3.up);
            if (angle <= _maxSlopeAngle)
            {
                _normal = n;
                Grounded = true;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            Grounded = false;
        }

        private void OnCollisionStay(Collision collision)
        {
            var n = collision.GetContact(0).normal;
            var angle = Vector3.Angle(n, Vector3.up);
            if (angle <= _maxSlopeAngle)
            {
                Grounded = true;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, transform.position + _normal * 3);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Project(transform.forward));
        }
    }
}

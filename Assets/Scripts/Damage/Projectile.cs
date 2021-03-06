using UnityEngine;

namespace Assets.Scripts.Damage
{
    public class Projectile : MonoBehaviour
    {
        public const float TIME_ALIVE = 5;

        public Vector3 Velocity { get; private set; }
        public GameObject Host { get; set; }

        [SerializeField] private Rigidbody _rigidBody;
        private float _timeCreated;

        private void OnEnable()
        {
            _timeCreated = Time.time;
        }

        public void Init(Vector3 velocity, GameObject host)
        {
            Velocity = velocity;
            Host = host;
            transform.LookAt(Velocity+transform.position);
        }

        private void Update()
        {
            _rigidBody.MovePosition(_rigidBody.position + Velocity * Time.timeScale);
            if (_timeCreated + TIME_ALIVE < Time.time)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject != Host)
            {
                var health = other.GetComponent<AHealth>();
                if (health != null)
                {
                    health.TakeDamage();
                }
                Destroy(gameObject);
            }
        }
    }
}

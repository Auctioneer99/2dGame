using Assets.Scripts.Damage;
using UnityEngine;

namespace Assets.Scripts
{
    public class ShootingShit : MonoBehaviour, IShooting
    {
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _bulletSource;
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private float _cooldown = 3;
        private float _lastExcecutionTime = 0;

        public void Shoot(Vector3 deltaVector)
        {
            _lastExcecutionTime -= Time.deltaTime;
            if (_lastExcecutionTime < 0)
            {
                _lastExcecutionTime = _cooldown;
                var projectile = Instantiate(_projectilePrefab, _bulletSource.transform.position, Quaternion.identity);
                projectile.Init(deltaVector.normalized * _projectileSpeed, _player);
            }
        }
    }

    public interface IShooting
    {
        void Shoot(Vector3 deltaVector);
    }
}

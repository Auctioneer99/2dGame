using Assets.Scripts.Damage;
using UnityEngine;

namespace Assets.Scripts
{
    public class MouseInput : MonoBehaviour
    {
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _bulletSource;
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _armsTarget;
        [SerializeField] private float _distanceFromObject;

        private void Update()
        {
            var mousePos = Input.mousePosition;
            mousePos.z = _distanceFromObject;

            Vector3 pos = _camera.ScreenToWorldPoint(mousePos);
            _armsTarget.position = pos;


            var delta = pos - _bulletSource.transform.position;
            delta.z = 0;
            if (Input.GetMouseButtonDown(0) && Time.timeScale > 0)
            {
                var projectile = Instantiate(_projectilePrefab, _bulletSource.transform.position, Quaternion.identity);
                projectile.Init(delta.normalized * _projectileSpeed , _player);
            }
        }
    }
}

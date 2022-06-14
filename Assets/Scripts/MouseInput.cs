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
        [SerializeField] private GameObject _lol;
        [SerializeField] private GameObject _lol2;

        private void Update()
        {
            var mousePos = Input.mousePosition;
            mousePos.z = _distanceFromObject;

            Vector3 pos = _camera.ScreenToWorldPoint(mousePos);

            var delta = pos - _bulletSource.transform.position;

            var angle = Quaternion.FromToRotation(Vector3.up, delta);
            var eulers = angle.eulerAngles;
            eulers.y = 0;
            eulers.x = 0;
            _armsTarget.rotation = Quaternion.Euler(eulers);

            if (Input.GetMouseButtonDown(0) && Time.timeScale > 0)
            {
                var projectile = Instantiate(_projectilePrefab, _bulletSource.transform.position, Quaternion.identity);
                projectile.Init(eulers.normalized * _projectileSpeed , _player);
                Debug.Log(eulers);
            }
            _lol2.transform.position = _lol.transform.position;
            Debug.Log(_lol2.transform.position);
        }
    }
}

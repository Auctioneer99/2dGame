using Assets.Scripts.Damage;
using UnityEngine;

namespace Assets.Scripts
{
    public class MouseInput : MonoBehaviour
    {
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private GameObject _player;
        [SerializeField] private float _projectileSpeed;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && Time.timeScale > 0)
            {
                Vector3 deltaVector = Input.mousePosition;
                deltaVector.x = deltaVector.x / (Screen.width / 2) - 1;
                deltaVector.y = deltaVector.y / (Screen.height / 2) - 1;
                deltaVector.z = 0;

                var projectile = Instantiate(_projectilePrefab, _player.transform.position, Quaternion.identity);
                projectile.Init(deltaVector.normalized * _projectileSpeed , _player);
            }
        }
    }
}

using Assets.Scripts.Damage;
using UnityEngine;

namespace Assets.Scripts
{
    public class MouseInput : MonoBehaviour
    {
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private GameObject _player;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var projectile = Instantiate(_projectilePrefab, _player.transform);
                projectile.Init(new Vector3(0, 0, 0.1f), _player);
            }
        }
    }
}

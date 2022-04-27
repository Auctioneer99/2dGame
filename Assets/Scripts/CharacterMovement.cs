using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private SurfaceSlider _surfaceSlider;
        [SerializeField] private float _speed;

        [SerializeField] private float _gravity;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _jumpAppend;
        [SerializeField] private float _gravityMultiplier;

        private float _ySpeed;

        public void Move(Vector3 direction)
        {
            Vector3 directionAlongSurface = _surfaceSlider.Project(direction.normalized);
            Vector3 offset = directionAlongSurface * (_speed * Time.deltaTime);

            _rigidBody.MovePosition(_rigidBody.position + offset);
        }

        public void Jump()
        {
            if (_surfaceSlider.Grounded == true)
            {
                 _ySpeed = Mathf.Sqrt(_jumpHeight * 2 * Mathf.Abs(_gravity) * _gravityMultiplier) * -Mathf.Sign(_gravity);
            }
            else
            {
                if (_ySpeed > 0)
                {
                    _ySpeed += _jumpAppend * _gravityMultiplier * Time.deltaTime;
                }
            }
        }

        public void ChangeGravity()
        {
            _gravity *= -1;
            _surfaceSlider.ChangeGravityVector(new Vector3(0, _gravity, 0));
        }

        private void Update()
        {
            float gravity = _gravity * _gravityMultiplier * Time.deltaTime;
            _ySpeed += gravity;

            if (_surfaceSlider.Grounded && _ySpeed * -Mathf.Sign(_gravity) <= 0f)
            {
                _ySpeed = 0f;
            }
            _rigidBody.MovePosition(_rigidBody.position + new Vector3(0, _ySpeed) * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            var normal = collision.GetContact(0).normal;

            var angle = Vector3.Angle(normal, Vector3.up);

            _ySpeed = _ySpeed + Mathf.Cos(angle) * _ySpeed * Mathf.Sign(_ySpeed);
        }
    }
}
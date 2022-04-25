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
            //_rigidBody.position = _rigidBody.position + offset;
        }

        public void Jump()
        {
            if (_surfaceSlider.Grounded == true)
            {
                 _ySpeed = Mathf.Sqrt(_jumpHeight * 2 * _gravity * _gravityMultiplier);
            }
            else
            {
                if (_ySpeed > 0)
                {
                    _ySpeed += _jumpAppend * _gravityMultiplier * Time.deltaTime;
                }
            }
        }

        private void Update()
        {
            float gravity = Physics.gravity.y * _gravityMultiplier * Time.deltaTime;
            _ySpeed += gravity;

            if (_surfaceSlider.Grounded && _ySpeed <= 0f)
            {
                _ySpeed = 0f;
            }
            _rigidBody.MovePosition(_rigidBody.position + new Vector3(0, _ySpeed) * Time.deltaTime);
            //_rigidBody.AddForce(Vector3.up * _jumpHeight, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            var normal = collision.GetContact(0).normal;
            if (_ySpeed >= 0f)
            {
                var angle = Vector3.Angle(normal, Vector3.down);
                if (angle <= 90)
                {
                    _ySpeed = _ySpeed - (_ySpeed * Mathf.Cos(angle));
                }
            }
        }
    }
}
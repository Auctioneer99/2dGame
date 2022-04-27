using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private Camera _camera;
        [SerializeField] private float _range;
        [SerializeField] private float _distanceFromObject;

        void Update()
        {
            Vector3 deltaVector = Input.mousePosition;
            deltaVector.x = deltaVector.x / (Screen.width / 2) - 1;
            deltaVector.y = deltaVector.y / (Screen.height / 2) - 1;
            deltaVector.z = 0;

            Move(deltaVector);
        }

        public void Move(Vector3 deltaVector)
        {
            deltaVector *= _range;
            deltaVector.z = -_distanceFromObject;

            _camera.transform.position = _target.transform.position + deltaVector;
            Quaternion lookRotation = Quaternion.LookRotation(_target.transform.position - _camera.transform.position, Vector3.up);
            //_camera.transform.rotation = lookRotation;
        }
    }
}

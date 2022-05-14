﻿using UnityEngine;

namespace Assets.Scripts
{
    public class KeyboardInput : MonoBehaviour
    {
        bool pause = false;
        [SerializeField] private PlayerController _controller;

        private void Update()
        {
            _controller.SetInputs(
                Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical"),
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical")
                );

            _controller.HandleWalking(Input.GetKey(KeyCode.LeftArrow), Input.GetKey(KeyCode.RightArrow));

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _controller.HandleJumping();
            }
        }
    }
}

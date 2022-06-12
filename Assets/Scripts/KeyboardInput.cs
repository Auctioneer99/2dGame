using UnityEngine;

namespace Assets.Scripts
{
    public class KeyboardInput : MonoBehaviour
    {
        [SerializeField] private PlayerController _controller;

        private void Update()
        {
            if (_controller)
                if (!_controller.dead)
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
}

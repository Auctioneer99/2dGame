using UnityEngine;

namespace Assets.Scripts
{
    public class KeyboardInput : MonoBehaviour
    {
        [SerializeField] private CharacterMovement _movement;

        private void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");

            _movement.Move(new Vector3(0, 0, horizontal));

            bool jump = Input.GetButton("Jump");
            if (jump)
            {
                _movement.Jump();
            }
        }
    }
}

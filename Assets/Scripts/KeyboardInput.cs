using UnityEngine;

namespace Assets.Scripts
{
    public class KeyboardInput : MonoBehaviour
    {
        bool pause = false;
        [SerializeField] private CharacterMovement _movement;

        private void FixedUpdate()
        {
            float horizontal = Input.GetAxis("Horizontal");

            _movement.Move(new Vector3(0, 0, horizontal));

            bool jump = Input.GetButton("Jump");
            if (jump)
            {
                _movement.Jump();
            }

            bool changeFravity = Input.GetKey(KeyCode.E);
            if (changeFravity)
            {
                _movement.ChangeGravity();
            }
        }
    }
}

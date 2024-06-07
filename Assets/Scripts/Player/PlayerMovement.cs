using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using TouchControlsKit;       

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform _cam;
    [SerializeField] private ThirdPersonCharacter _thirdPersonCharacter;

    private Vector3 _camForward;
    private Vector3 _move;
    private bool _jump;

    private void Update()
    {
        if (!_jump)
        {
            _jump = Input.GetButtonDown("Jump");
        }

        if (TCKInput.GetAction(InputParametrs.Jump, EActionEvent.Down))
        {
            Jump();
        }
    }


    private void FixedUpdate()
    {
        Move();
    }

    public void SetChatacter(ThirdPersonCharacter thirdPersonCharacter)
    {
        _thirdPersonCharacter = thirdPersonCharacter;
    }

    private void Move()
    {
        float h = 0;
        float v = 0;

        Vector2 move = TCKInput.GetAxis(InputParametrs.Joystick);

        if (Input.GetAxis("Horizontal") != 0)
            h = Input.GetAxis("Horizontal");
        else if (move.x != 0)
            h = move.x;

        if (Input.GetAxis("Vertical") != 0)
            v = Input.GetAxis("Vertical");
        else if (move.y != 0)
            v = move.y;

        bool crouch = Input.GetKey(KeyCode.C);


        if (_cam != null)
        {
            _camForward = Vector3.Scale(_cam.forward, new Vector3(1, 0, 1)).normalized;
            _move = v * _camForward + h * _cam.right;
        }
        else
        {
            _move = v * Vector3.forward + h * Vector3.right;
        }

        _thirdPersonCharacter.Move(_move, crouch, _jump);
        _jump = false;
    }

    private void Jump()
    {
        _jump = true;
    }
}

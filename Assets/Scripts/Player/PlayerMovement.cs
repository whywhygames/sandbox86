using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Button _jumpButton;
    [SerializeField] private Transform _cam;

    private ThirdPersonCharacter _thirdPersonCharacter;
    private Vector3 _camForward;
    private Vector3 _move;
    private bool _jump;

    private void OnEnable()
    {
        _jumpButton.onClick.AddListener(Jump);
    }

    private void OnDisable()
    {
        _jumpButton.onClick.RemoveListener(Jump);
    }

    private void Update()
    {
        if (!_jump)
        {
            _jump = Input.GetButtonDown("Jump");
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

        if (Input.GetAxis("Horizontal") != 0)
            h = Input.GetAxis("Horizontal");
        else if (_joystick.Horizontal != 0)
            h = _joystick.Horizontal;

        if (Input.GetAxis("Vertical") != 0)
            v = Input.GetAxis("Vertical");
        else if (_joystick.Vertical != 0)
            v = _joystick.Vertical;

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

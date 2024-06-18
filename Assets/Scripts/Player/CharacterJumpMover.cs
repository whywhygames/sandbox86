using System.Collections;
using System.Collections.Generic;
using TouchControlsKit;
using UnityEngine;

public class CharacterJumpMover : MonoBehaviour
{
    [SerializeField] private Transform _legs;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;

    private bool _isJump;
    Vector2 move;

    private void FixedUpdate()
    {
        _isJump = !Physics.CheckSphere(_legs.position, _radius / 10, _groundMask);

        if (_isJump == false)
            move = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (_isJump)
        {
            move = TCKInput.GetAxis(InputParametrs.Joystick);
             // var vfer = new Vector3(transform.forward.x * _speed, 0, transform.forward.z * _speed);
            // transform.Translate(vfer);
            Debug.Log(transform.forward);
            _rigidbody.velocity = new Vector3(transform.forward.x * Mathf.Abs(move.normalized.magnitude) * _speed, _rigidbody.velocity.y, transform.forward.z * Mathf.Abs(move.normalized.magnitude) * _speed);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawSphere(_legs.transform.position, _radius / 10);
    }
}

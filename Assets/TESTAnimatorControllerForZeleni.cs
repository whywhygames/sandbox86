using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTAnimatorControllerForZeleni : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _animatorPanel;

    public void Activate()
    {
        _animatorPanel.SetActive(true);
        _animator.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        _animator.gameObject.SetActive(false);
        _animatorPanel.SetActive(false);
    }

    private bool _isRun = false;
    public void Run()
    {
        if (_isRun == false)
        {
            _isRun = true;
            _animator.SetBool("IsRun", _isRun); 
        }
        else
        {
            _isRun = false;
            _animator.SetBool("IsRun", _isRun) ;
        }
    }

    public void Attack()
    {
        _animator.SetTrigger(nameof(Attack));
    }

    public void JumpAttack()
    {
        _animator.SetTrigger(nameof(JumpAttack));
    }

    public void Died()
    {
        _animator.SetTrigger(nameof(Died));
    }

    public void GetHit()
    {
        _animator.SetTrigger(nameof(GetHit));
    }

    public void Hook()
    {
        _animator.SetTrigger(nameof(Hook));
    }

    public void Jump()
    {
        _animator.SetTrigger(nameof(Jump));
    }
}

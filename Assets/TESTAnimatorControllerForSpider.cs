using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTAnimatorControllerForSpider : MonoBehaviour
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
        _animatorPanel.SetActive(false);
        _animator.gameObject.SetActive(false);
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
            _animator.SetBool("IsRun", _isRun);
        }
    }

    public void Attack()
    {
        _animator.SetTrigger(nameof(Attack));
    }

    public void Died()
    {
        _animator.SetTrigger(nameof(Died));
    }
}

using CoverShooter;
using System.Collections.Generic;
using UnityEngine;

public class DogWeapon : MonoBehaviour
{
    [Header ("Components:")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Gun _gun;
    [SerializeField] private CharacterMotor _characterMotor;

    [SerializeField] private Transform _target;

    [Header("Paramters:")]
    [SerializeField] private bool _isShoot;

    private void Start()
    {
        _gun.Character = _characterMotor;
    }

    private void Update()
    {
        if (_isShoot)
            Fire();
    }

    public void Open()
    {
        _animator.SetBool("IsFire", true);
        _isShoot = true;
    }

    public void Fire()
    {
        _gun.Allow(true);
        _gun.FireWhenReady();
    }

    public void Close()
    {
        _animator.SetBool("IsFire", false);
        _isShoot = false;
    }
}

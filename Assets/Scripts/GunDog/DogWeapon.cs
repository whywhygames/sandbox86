using CoverShooter;
using System.Collections.Generic;
using UnityEngine;

public class DogWeapon : MonoBehaviour
{
    [Header ("Components:")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Gun _gun;
    [SerializeField] private CharacterMotor _characterMotor;

    [Header("Paramters:")]
    [SerializeField] private bool _isShoot;
    [SerializeField] private float _reloadTime;

    private bool _isReload;
    private bool _isEnject;
    private bool _isRechaber;
    private float _elapsedReloadTime;

    private void Start()
    {
        _gun.Character = _characterMotor;
    }

    private void Update()
    {
        if (_isReload)
        {
            _elapsedReloadTime += Time.deltaTime;

            if (_isEnject == false)
            {
                _gun.NotifyEject();
                _isEnject = true;
            }

            if (_elapsedReloadTime > _reloadTime / 2 && _isRechaber == false)
            {
                _gun.NotifyRechamber();
                _isRechaber = true;
            }

            if (_elapsedReloadTime > _reloadTime )
            {
                _elapsedReloadTime = 0;
                _isReload = false;
                _isEnject = false;
                _isRechaber = false;
            }

            return;
        }

        if (_isShoot)
            Fire();

        if (_gun.LoadedBullets == 0)
        {
            _isReload = true;
            _gun.LoadMagazine();
        }
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

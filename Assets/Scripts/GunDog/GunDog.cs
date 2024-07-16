using CoverShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDog : MonoBehaviour
{
    [SerializeField] private Gun _gun;

    private void Update()
    {
        _gun.Allow(true);
        //_gun.Fired();
        _gun.FireWhenReady();
    }
}

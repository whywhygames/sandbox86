using CoverShooter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrenadeInventoryCounter : MonoBehaviour
{
    [field: SerializeField] public int CountGrenade;

    [SerializeField] private ThirdPersonController _thirdPersonController;

    public event UnityAction ChangeCount;

    public void OnEnable()
    {
        _thirdPersonController.ThrowGrenadeEvent += UpdateCounter;
    }

    private void OnDisable()
    {
        _thirdPersonController.ThrowGrenadeEvent -= UpdateCounter;
    }

    private void UpdateCounter()
    {
        CountGrenade--;
        ChangeCount?.Invoke();
    }
}

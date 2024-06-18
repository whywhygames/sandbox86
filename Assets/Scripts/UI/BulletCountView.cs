using CoverShooter;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletCountView : MonoBehaviour
{
    [SerializeField] private Gun _targetGun;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _targetGun.ChangeBullets += UpdateText;
    }

    private void Start()
    {
        _text.text = $"{_targetGun.LoadedBullets}/{_targetGun.BulletInventory}";
    }

    private void OnDisable()
    {
        _targetGun.ChangeBullets -= UpdateText;
    }

    private void UpdateText()
    {
        _text.text = $"{_targetGun.LoadedBullets}/{_targetGun.BulletInventory}";
    }
}

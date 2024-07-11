using CoverShooter;
using UnityEngine;
using UnityEngine.UI;

public class LaserCountView : MonoBehaviour
{
    [SerializeField] private Gun _targetGun;
    [SerializeField] private Slider _slider;

    private void OnEnable()
    {
        _targetGun.ChangeBullets += UpdateText;
    }

    private void Start()
    {
        _slider.maxValue = _targetGun.LoadedBullets;
        _slider.value = _targetGun.LoadedBullets;
    }

    private void OnDisable()
    {
        _targetGun.ChangeBullets -= UpdateText;
    }

    private void UpdateText()
    {
        _slider.value = _targetGun.LoadedBullets;
    }
}

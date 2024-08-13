using CoverShooter;
using UnityEngine;
using UnityEngine.UI;

public class LaserCountView : MonoBehaviour
{
    [SerializeField] private Gun _targetGun;
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _fillImage;

    private void OnEnable()
    {
        _targetGun.ChangeBullets += UpdateText;
    }

    private void Start()
    {
     //   _slider.maxValue = _targetGun.LoadedBullets;
       // _slider.value = _targetGun.LoadedBullets;
    }

    private void OnDisable()
    {
        _targetGun.ChangeBullets -= UpdateText;
    }

    private void UpdateText()
    {
        _fillImage.fillAmount = (float)_targetGun.LoadedBullets / _targetGun.MagazineSize;
        //_slider.value = _targetGun.LoadedBullets;
    }
}

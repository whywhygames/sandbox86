using CoverShooter;
using UnityEngine;

public class CharacterControllerUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _fireButton;
    [SerializeField] private CanvasGroup _closeZoomButton;
    [SerializeField] private CanvasGroup _reloadButton;
    [SerializeField] private CharacterMotor _characterMotor;

    private void Update()
    {
        if (_characterMotor.EquippedWeapon.Gun != null)
        {
            _fireButton.Activate();
            _reloadButton.Activate();
        }
        else
        {
            _fireButton.Deactivate();
            _reloadButton.Deactivate();
        }

        if (_characterMotor.IsAimingGun)
        {
            _closeZoomButton.Activate();
        }
        else
        {
            _closeZoomButton.Deactivate();
        }
    }
}

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
        if (_characterMotor.HasGrenadeInHand)
        {
            _reloadButton.Deactivate();
            _fireButton.Activate();
        }
        else if (_characterMotor.WeaponEquipState == WeaponEquipState.equipped)
        {
            WeaponType type = WeaponType.Sniper;

            if (!_characterMotor.IsEquipped)
                return;
            else if (_characterMotor.Weapon.RightMelee != null)
                type = _characterMotor.Weapon.RightMelee.Type;
            else if (_characterMotor.Weapon.LeftMelee != null)
                type = _characterMotor.Weapon.LeftMelee.Type;

            switch (type)
            {
                case WeaponType.Pistol:
                    _fireButton.Activate();
                    _reloadButton.Activate();
                    _closeZoomButton.Activate();
                    break;

                case WeaponType.Rifle:
                    _fireButton.Activate();
                    _reloadButton.Deactivate();
                    _closeZoomButton.Activate();
                    break;

                case WeaponType.Shotgun:
                    _fireButton.Activate();
                    _reloadButton.Activate();
                    _closeZoomButton.Activate();
                    break;

                case WeaponType.Sniper:
                    _fireButton.Activate();
                    _reloadButton.Activate();
                    _closeZoomButton.Activate();
                    break;

                case WeaponType.Fist:
                    _fireButton.Activate();
                    _closeZoomButton.Deactivate();
                    break;
            }
        }
        else
        {
            _fireButton.Deactivate();
            _reloadButton.Deactivate();
            _closeZoomButton.Deactivate();
        }

        /*  if (_characterMotor.IsAimingGun)
          {
              _closeZoomButton.Activate();
          }
          else
          {
              _closeZoomButton.Deactivate();
          }*/
    }

    private void CheckWeaponType()
    {
       /* _characterMotor.EquippedWeapon.HasMelee == WeaponType.Fist;
        _characterMotor.EquippedWeapon.Gun.Type == WeaponType.Pistol;*/
    }
}

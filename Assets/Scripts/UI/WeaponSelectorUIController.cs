using System.Collections;
using System.Collections.Generic;
using TouchControlsKit;
using UnityEngine;

public class WeaponSelectorUIController : MonoBehaviour
{
    [SerializeField] private GameObject _fistOutline;
    [SerializeField] private GameObject _pistolOutline;
    [SerializeField] private GameObject _rifleOutline;
    [SerializeField] private GameObject _sniperOutline;
    [SerializeField] private GameObject _azotBlasterOutline;
    [SerializeField] private GameObject _grenadeOutline;

    private GameObject _currentActiveOutline;

    private void Update()
    {
        if (TCKInput.GetAction(InputParametrs.Weapon2BUTTON, EActionEvent.Down))
        {
            ActivateOutline(_fistOutline);
        }
        if (TCKInput.GetAction(InputParametrs.Weapon3BUTTON, EActionEvent.Down))
        {
            ActivateOutline(_pistolOutline);
        }
        if (TCKInput.GetAction(InputParametrs.Weapon4BUTTON, EActionEvent.Down))
        {
            ActivateOutline(_rifleOutline);
        }
        if (TCKInput.GetAction(InputParametrs.Weapon5BUTTON, EActionEvent.Down))
        {
            ActivateOutline(_sniperOutline);
        }
        if (TCKInput.GetAction(InputParametrs.Weapon6BUTTON, EActionEvent.Down))
        {
            ActivateOutline(_azotBlasterOutline);
        }
    /*    if (TCKInput.GetAction(InputParametrs.GrenadeBUTTON, EActionEvent.Down))
        {
            ActivateOutline(_grenadeOutline);
        }*/
    }

    private void ActivateOutline(GameObject newOutline)
    {
        if (_currentActiveOutline != null)
            _currentActiveOutline.SetActive(false);

        _currentActiveOutline = newOutline;
        _currentActiveOutline.SetActive(true);
    }
}

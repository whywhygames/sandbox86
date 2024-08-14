using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private CanvasGroup _characterSelector;
    [SerializeField] private CanvasGroup _menu;
    [SerializeField] private Button _openButton;

    private bool _isOpen;

    private void OnEnable()
    {
        _openButton.onClick.AddListener(Show);
    }

    private void OnDisable()
    {
        _openButton.onClick.RemoveListener(Show);
    }

    private void Show()
    {
        if (_isOpen)
        {
            _isOpen = false;
            _openButton.transform.eulerAngles = new Vector3(0, 0, 0);
            _menu.Deactivate();
            _characterSelector.Activate();
        }
        else
        {
            _openButton.transform.eulerAngles = new Vector3(0, 0, 180);
            _isOpen = true;
            _menu.Activate();
            _characterSelector.Deactivate();
        }
    }
}

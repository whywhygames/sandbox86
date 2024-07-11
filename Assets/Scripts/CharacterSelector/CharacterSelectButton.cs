using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectButton : MonoBehaviour
{
    [SerializeField] private CharacterSelector _characterSelector;
    [SerializeField] private Image _iconImage;

    private CharacterViewConfigure _configure;
    private Button _button;

    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClickHandler);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClickHandler);
    }

    public void Initialize(CharacterViewConfigure configure, CharacterSelector selector)
    {
        _characterSelector = selector;
        _configure = configure;
        _iconImage.sprite = _configure.IconButton;
    }

    private void OnClickHandler()
    {
        _characterSelector.OnClickHandler(_configure.CharacterType);
    }
}

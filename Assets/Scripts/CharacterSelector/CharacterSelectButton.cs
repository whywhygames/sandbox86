using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectButton : MonoBehaviour
{
    [SerializeField] private CharacterSelector _characterSelector;
    [SerializeField] private Image _iconImage;
    [SerializeField] private GameObject _outline;

    private CharacterViewConfigure _configure;
    private Button _button;

    public CharacterViewConfigure Configure { get => _configure; private set => _configure = value; }

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
        _iconImage.sprite = _configure.Icon;
    }

    private void OnClickHandler()
    {
        _characterSelector.OnClickHandler(_configure.CharacterType);
    }

    public void SetOutline(bool isActive)
    {
        _outline.SetActive(isActive);
    }
}

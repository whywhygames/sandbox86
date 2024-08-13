using System.Collections.Generic;
using TMPro;
using TouchControlsKit;
using UnityEngine;
using UnityEngine.Events;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField] private PlayerBootstrap _playerBootstrap;
    [SerializeField] private List<CharacterViewConfigure> _configures = new List<CharacterViewConfigure>();
    [SerializeField] private CharacterSelectButton _burronPrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private CanvasGroup _canvasGroup;

    private List<CharacterSelectButton> _selectButtons = new List<CharacterSelectButton>();
    private CharacterSelectButton _currentSelectButton;
    private CharacterType _currentSelectCharacter;

    public event UnityAction ChangeCharacter;

    private void Start()
    {
        ClearList();

        FillList();

        OnClickHandler(_playerBootstrap.Type);
    }

    private void ClearList()
    {
        if (_selectButtons.Count > 0)
        {
            foreach (CharacterSelectButton button in _selectButtons)
            {
                Destroy(button.gameObject);
            }

            _selectButtons.Clear();
        }
    }

    private void FillList()
    {
        foreach (var configureButtons in _configures)
        {
            var button = Instantiate(_burronPrefab, _container);
            button.Initialize(configureButtons, this);
            _selectButtons.Add(button);
        }
    }

    private void Update()
    {
      /*  if (TCKInput.GetButtonDown(InputParametrs.SelectCharacterBUTTON))
        {
            Open();
        }*/
    }

    public void OnClickHandler(CharacterType characterType)
    {
        if (_currentSelectButton != null)
            _currentSelectButton.SetOutline(false);

        _currentSelectCharacter = characterType;

       foreach (var button in _selectButtons)
        {
            if (button.Configure.CharacterType == _currentSelectCharacter)
            {
                button.SetOutline(true);
                _currentSelectButton = button;
            }
        }

        if (_playerBootstrap.Type != _currentSelectCharacter)
        {
            _playerBootstrap.SetCharater(_currentSelectCharacter);
            ChangeCharacter?.Invoke();
        }
    }

    private void Open()
    {
        _canvasGroup.Activate();
    }

    private void Close()
    {
        _canvasGroup.Deactivate();
    }
}

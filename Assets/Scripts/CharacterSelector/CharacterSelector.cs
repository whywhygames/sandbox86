using System.Collections.Generic;
using TMPro;
using TouchControlsKit;
using UnityEngine;
using UnityEngine.Events;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField] private PlayerBootstrap _playerBootstrap;
    [SerializeField] private List<CharacterViewConfigure> _configuresButtons = new List<CharacterViewConfigure>();
    [SerializeField] private CharacterSelectButton _burronPrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private CanvasGroup _canvasGroup;

    private List<CharacterSelectButton> _selectButtons = new List<CharacterSelectButton>();
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
        foreach (var configureButtons in _configuresButtons)
        {
            var button = Instantiate(_burronPrefab, _container);
            button.Initialize(configureButtons, this);
        }
    }

    private void SetCharacter()
    {
        _playerBootstrap.SetCharater(_currentSelectCharacter);
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
        _currentSelectCharacter = characterType;

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

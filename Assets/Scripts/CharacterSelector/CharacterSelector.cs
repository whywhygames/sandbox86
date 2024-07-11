using System.Collections.Generic;
using TMPro;
using TouchControlsKit;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField] private PlayerBootstrap _playerBootstrap;
    [SerializeField] private List<CharacterViewConfigure> _configuresButtons = new List<CharacterViewConfigure>();
    [SerializeField] private CharacterSelectButton _burronPrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private Image _ñharacterImage;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Button _closePanelButton;
    [SerializeField] private Button _selectCharacterButton;
    [SerializeField] private TMP_Text _selectCharacterButtonText;

    private List<CharacterSelectButton> _selectButtons = new List<CharacterSelectButton>();
    private CharacterType _currentSelectCharacter;

    private void Start()
    {
        SubscribeButton();

        ClearList();

        FillList();

        OnClickHandler(_playerBootstrap.Type);
    }

    private void SubscribeButton()
    {
        _closePanelButton.onClick.AddListener(Close);
        _selectCharacterButton.onClick.AddListener(SetCharacter);
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
        _selectCharacterButtonText.text = "YOU";
        _selectCharacterButton.interactable = false;
        _playerBootstrap.SetCharater(_currentSelectCharacter);
    }

    private void Update()
    {
        if (TCKInput.GetButtonDown(InputParametrs.SelectCharacter))
        {
            Open();
        }
    }

    public void OnClickHandler(CharacterType characterType)
    {
        _currentSelectCharacter = characterType;
       // _playerBootstrap.SetCharater(characterType);

        if (_playerBootstrap.Type == _currentSelectCharacter)
        {
            _selectCharacterButtonText.text = "YOU";
            _selectCharacterButton.interactable = false;
        }
        else
        {
            _selectCharacterButtonText.text = "SELECT";
            _selectCharacterButton.interactable = true;
        }

        foreach (var configureButton in _configuresButtons)
            if (configureButton.CharacterType == characterType)
                _ñharacterImage.sprite = configureButton.IconCharacter;
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

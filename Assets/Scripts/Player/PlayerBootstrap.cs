using System.Collections;
using System.Collections.Generic;
using ThirdPersonCamera;
using UnityEngine;

public class PlayerBootstrap : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private List<CharacterVariant> _characters;
    [SerializeField] private CameraController _cameraController;

    private CharacterVariant _currentCharacter;

    private void Start()
    {
        SetCharater(CharacterType.Bom);
    }

    public void SetCharater(CharacterType characterType)
    {
        Transform oldTransform = transform;

        if (_currentCharacter != null)
        {
            if (_currentCharacter.CharacterType == characterType)
            {
                return;
            }

            oldTransform = _currentCharacter.transform;
            _currentCharacter.Deactivate();
       }

        _currentCharacter = GetCharacter(characterType);
        _currentCharacter.Activate(oldTransform);
        _cameraController.target = _currentCharacter.transform;

        _playerMovement.SetChatacter(_currentCharacter.ThirdPersonCharacter);
    }

    private CharacterVariant GetCharacter(CharacterType characterType)
    {
        foreach (CharacterVariant characterVariant in _characters)
        {
            if (characterVariant.CharacterType == characterType)
            {
                return characterVariant;
            }
        }

        return null;
    }
}

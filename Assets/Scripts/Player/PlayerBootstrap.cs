using System.Collections;
using System.Collections.Generic;
using ThirdPersonCamera;
using UnityEngine;

public class PlayerBootstrap : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private List<CharacterConfigure> _characters;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private Material _material;

    [field: SerializeField] public CharacterType Type = CharacterType.Bom;

    private CharacterConfigure _currentCharacter;

    private void Start()
    {
        SetCharater(Type);
    }

    public void SetCharater(CharacterType characterType)
    {
        if (_currentCharacter != null)
        {
            if (_currentCharacter.CharacterType == characterType)
            {
                return;
            }
        }

        _currentCharacter = GetCharacter(characterType);

        _skinnedMeshRenderer.sharedMesh = _currentCharacter.Mesh;
        _material.mainTexture = _currentCharacter.Texture;
        Type = characterType;
    }

    private CharacterConfigure GetCharacter(CharacterType characterType)
    {
        foreach (CharacterConfigure characterConfigure in _characters)
        {
            if (characterConfigure.CharacterType == characterType)
            {
                return characterConfigure;
            }
        }

        return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using ThirdPersonCamera;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBootstrap : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private List<CharacterConfigure> _characters;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private Material _material;
    [SerializeField] private Animator _animator;

    [field: SerializeField] public CharacterType Type = CharacterType.Assasin;

    private CharacterConfigure _currentCharacter;

    public event UnityAction ChangeCharacter;

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

        ChangeCharacter?.Invoke();
        _currentCharacter = GetCharacter(characterType);

        _skinnedMeshRenderer.sharedMesh = _currentCharacter.Mesh;
        _material.mainTexture = _currentCharacter.Texture;
        _animator.runtimeAnimatorController = _currentCharacter.Animator;
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

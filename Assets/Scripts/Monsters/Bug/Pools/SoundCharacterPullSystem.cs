using System.Collections.Generic;
using UnityEngine;

public abstract class SoundCharacterPullSystem : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private PlayerBootstrap _character;
    [Header("Assasin:")]
    [SerializeField] private List<AudioClip> _soundsAssasin = new List<AudioClip>();

    [Header("Technician:")]
    [SerializeField] private List<AudioClip> _soundsTechnician = new List<AudioClip>();

    [Header("Cowboy:")]
    [SerializeField] private List<AudioClip> _soundsCowboy = new List<AudioClip>();

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomSound()
    {
        switch (_character.Type)
        {
            case CharacterType.Assasin:
                if (_soundsAssasin.Count == 0)
                {
                    Debug.LogError($"Вы забыли добавить звуки в пулл для {gameObject.name}");
                    return;
                }

                _audioSource.PlayOneShot(_soundsAssasin[Random.Range(0, _soundsAssasin.Count)]);
                break;

            case CharacterType.Technician:
                if (_soundsTechnician.Count == 0)
                {
                    Debug.LogError($"Вы забыли добавить звуки в пулл для {gameObject.name}");
                    return;
                }

                _audioSource.PlayOneShot(_soundsTechnician[Random.Range(0, _soundsTechnician.Count)]);
                break;

            case CharacterType.Cowboy:
                if (_soundsCowboy.Count == 0)
                {
                    Debug.LogError($"Вы забыли добавить звуки в пулл для {gameObject.name}");
                    return;
                }

                _audioSource.PlayOneShot(_soundsCowboy[Random.Range(0, _soundsCowboy.Count)]);
                break;
        }
    }
}

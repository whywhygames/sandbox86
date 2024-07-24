using CoverShooter;
using System.Collections.Generic;
using UnityEngine;

public abstract class SoundPullSystem : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _sounds = new List<AudioClip>();

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomSound()
    {
        if (_sounds.Count == 0)
        {
            Debug.LogError($"Вы забыли добавить звуки в пулл для {gameObject.name}");
            return;
        }

        _audioSource.PlayOneShot(_sounds[Random.Range(0, _sounds.Count)]);
    }
}
using System.Collections.Generic;
using UnityEngine;

public class CharacterBodySound : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _clips;
    [SerializeField] private AudioSource _audioSource;

    private void OnCollisionEnter(Collision collision)
    {
        if (_clips.Count > 0)
            _audioSource.PlayOneShot(_clips[Random.Range(0, _clips.Count)]);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogWeapon : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _clips = new List<AudioClip>();

    private Transform _target;

    public void Open()
    {

    }

    public void Fire()
    {

    }

    public void Close()
    {

    }
}

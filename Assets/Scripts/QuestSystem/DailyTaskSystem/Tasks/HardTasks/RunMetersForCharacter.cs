using CoverShooter;
using System;
using UnityEngine;

public class RunMetersForCharacter : DailyTask
{
    [SerializeField] private CharacterType _targetCaharacterType;
    private PlayerBootstrap _player;

    private CharacterMotor _characterMotor;
    private Vector3 _oldPosition;
    private Vector3 _currentPosition;
    private Vector3 _startPosition;
    private float _distanceX = 0;
    private float _distanceZ = 0;

    private void Start()
    {
        _characterMotor = FindAnyObjectByType<CharacterMotor>();
        _player = FindObjectOfType<PlayerBootstrap>();
        _player.ChangeCharacter += OnChangeCharacter;
        _startPosition = _characterMotor.transform.position;
    }

    private void OnDisable()
    {
        _player.ChangeCharacter -= OnChangeCharacter;
    }

    private void Update()
    {
        if (IsCompleted)
            return;

        if (_player.Type == _targetCaharacterType)
        {
            _currentPosition = _startPosition - _characterMotor.transform.position;

            var tmpDistX = Mathf.Abs(_currentPosition.x - _oldPosition.x);
            var tmpDistZ = Mathf.Abs(_currentPosition.z - _oldPosition.z);

            _oldPosition = _currentPosition;

            _distanceX += tmpDistX;
            _distanceZ += tmpDistZ;

            if (tmpDistX == 0 && tmpDistZ > 0) { _distanceX = 0; }
            if (tmpDistX > 0 && tmpDistZ == 0) { _distanceZ = 0; }

            //ChangeEquelCounter(_distanceX + _distanceZ);
            AddCounter(tmpDistX + tmpDistZ);

            if (CurrentCount >= TargerCount)
            {
                GiveReward();
            }
        }
    }

    private void OnChangeCharacter()
    {
        _startPosition = _characterMotor.transform.position;
        _currentPosition = _startPosition - _characterMotor.transform.position;
        _oldPosition = _currentPosition;
    }
}

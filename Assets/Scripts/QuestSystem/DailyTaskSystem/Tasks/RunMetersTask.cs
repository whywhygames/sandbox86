using CAS;
using CoverShooter;
using UnityEngine;

public class RunMetersTask : DailyTask
{
    [SerializeField] private CharacterMotor _charater;
    [SerializeField] private float _meters;
    
    private float _currentMeters;
    private Vector3 _oldPosition;
    private Vector3 _currentPosition;
    private Vector3 _startPosition;
    private float _distanceX = 0;
    private float _distanceZ = 0;

    private void Start()
    {
        _charater = FindAnyObjectByType<CharacterMotor>();
        _startPosition = _charater.transform.position;
    }

    private void Update()
    {
        if (IsCompleted)
            return;

        _currentPosition = _startPosition - _charater.transform.position;

        var tmpDistX = Mathf.Abs(_currentPosition.x - _oldPosition.x);
        var tmpDistZ = Mathf.Abs(_currentPosition.z - _oldPosition.z);

        _oldPosition = _currentPosition;

        _distanceX += tmpDistX;
        _distanceZ += tmpDistZ;

        if (tmpDistX == 0 && tmpDistZ > 0) { _distanceX = 0; }
        if (tmpDistX > 0 && tmpDistZ == 0) { _distanceZ = 0; }

        _currentMeters = _distanceX + _distanceZ;

        if (_currentMeters >= 100)
        {
            GiveReward();
        }
    }
}

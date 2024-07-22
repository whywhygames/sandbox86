using CoverShooter;
using UnityEngine;

public class RunMetersForWeapon : DailyTask
{
    [SerializeField] private WeaponType _targetWeaponType;

    private CharacterMotor _characterMotor;
    private Vector3 _oldPosition;
    private Vector3 _currentPosition;
    private Vector3 _startPosition;
    private float _distanceX = 0;
    private float _distanceZ = 0;



    private void Start()
    {
        _characterMotor = FindAnyObjectByType<CharacterMotor>();
        _startPosition = _characterMotor.transform.position;
    }

    private void Update()
    {
        if (IsCompleted)
            return;

        var equipped = _characterMotor.EquippedWeapon;

        WeaponType type;

        if (equipped.Gun != null)
            type = equipped.Gun.Type;
        else
            return;


        if (type == _targetWeaponType)
        {
            _currentPosition = _startPosition - _characterMotor.transform.position;

            var tmpDistX = Mathf.Abs(_currentPosition.x - _oldPosition.x);
            var tmpDistZ = Mathf.Abs(_currentPosition.z - _oldPosition.z);

            _oldPosition = _currentPosition;

            _distanceX += tmpDistX;
            _distanceZ += tmpDistZ;

            if (tmpDistX == 0 && tmpDistZ > 0) { _distanceX = 0; }
            if (tmpDistX > 0 && tmpDistZ == 0) { _distanceZ = 0; }

            ChangeEquelCounter(_distanceX + _distanceZ);

            if (CurrentCount >= TargerCount)
            {
                GiveReward();
            }
        }
    }
}

using CoverShooter;
using UnityEngine;

public class RunMetersForWeapon : DailyTask
{
    [SerializeField] private WeaponType _targetWeaponType;

    private CharacterMotor _characterMotor;
    private ThirdPersonInput _thirdPersonInput;
    private Vector3 _oldPosition;
    private Vector3 _currentPosition;
    private Vector3 _startPosition;

    private void Start()
    {
        _characterMotor = FindAnyObjectByType<CharacterMotor>();
        _thirdPersonInput = FindAnyObjectByType<ThirdPersonInput>();
        _thirdPersonInput.ChangeWeapon += OnChangeWeapon;
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

            AddCounter(tmpDistX + tmpDistZ);

            if (CurrentCount >= TargerCount)
            {
                GiveReward();
            }
        }
    }

    private void OnChangeWeapon()
    {
        _startPosition = _characterMotor.transform.position;
        _currentPosition = _startPosition - _characterMotor.transform.position;
        _oldPosition = _currentPosition;
    }
}

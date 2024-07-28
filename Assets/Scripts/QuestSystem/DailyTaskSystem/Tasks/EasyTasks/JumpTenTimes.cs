using CoverShooter;
using UnityEngine;

public class JumpTenTimes : DailyTask
{
    private CharacterMotor _playerInput;

    private void OnEnable()
    {
        _playerInput = FindObjectOfType<CharacterMotor>();
        _playerInput.Jump += OnJumped;
        Debug.Log(999);
    }

    private void OnDisable()
    {
        _playerInput.Jump -= OnJumped;
    }

    private void OnJumped()
    {
        if (IsCompleted)
            return;

        Debug.Log(1245);
        AddCounter(1);

        if (CurrentCount == TargerCount)
            GiveReward();
    }
}

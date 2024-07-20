using CoverShooter;

public class JumpTenTimes : DailyTask
{
    private CharacterMotor _playerInput;

    private void OnEnable()
    {
        _playerInput = FindObjectOfType<CharacterMotor>();
        _playerInput.Jump += OnJumped;
    }

    private void OnDisable()
    {
        _playerInput.Jump -= OnJumped;
    }

    private void OnJumped()
    {
        if (IsCompleted)
            return;

        AddCounter(1);

        if (CurrentCount == TargerCount)
            GiveReward();
    }
}

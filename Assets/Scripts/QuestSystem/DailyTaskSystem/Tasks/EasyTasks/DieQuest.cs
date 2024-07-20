public class DieQuest : DailyTask
{
    private PlayerHealth _playerHealth;

    private void OnEnable()
    {
        _playerHealth = FindObjectOfType<PlayerHealth>();
        _playerHealth.Died += OnDied;
    }

    private void OnDisable()
    {
        _playerHealth.Died -= OnDied;   
    }

    private void OnDied()
    {
        if (IsCompleted)
            return;

        AddCounter(1);

        if (CurrentCount == TargerCount)
            GiveReward();
    }
}

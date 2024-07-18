using UnityEngine;

public class TotalyFlyTask : DailyTask
{
    private CharacterJumpMover _jumpMover;

    private void Start()
    {
        _jumpMover = FindObjectOfType<CharacterJumpMover>();
    }

    private void Update()
    {
        if (IsCompleted)
            return;

        if (_jumpMover.IsJump)
        {
            AddCounter(Time.deltaTime);

            if (CurrentCount > TargerCount)
            {
                GiveReward();
            }
        }
    }
}

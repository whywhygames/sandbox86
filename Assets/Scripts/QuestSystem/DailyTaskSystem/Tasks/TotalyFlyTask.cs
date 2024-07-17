using UnityEngine;

public class TotalyFlyTask : DailyTask
{
    [SerializeField] private float _second;
    
    private CharacterJumpMover _jumpMover;
    private float _elapsedTime;

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
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime > _second)
            {
                GiveReward();
            }
        }
    }
}

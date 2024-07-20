using UnityEngine;

public class ChangeCharaterFiveTimes : DailyTask
{
    private CharacterSelector _characterSelector;

    private void OnEnable()
    {
        _characterSelector = FindObjectOfType<CharacterSelector>();
        _characterSelector.ChangeCharacter += OnChange;
    }

    private void OnDisable()
    {
        _characterSelector.ChangeCharacter -= OnChange;
    }

    private void OnChange()
    {
        if (IsCompleted)
            return;

        AddCounter(1);

        if (CurrentCount == TargerCount)
            GiveReward();
    }
}

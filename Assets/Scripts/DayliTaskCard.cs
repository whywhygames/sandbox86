using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayliTaskCard : MonoBehaviour
{
    [SerializeField] private Image _rewardIcon;
    [SerializeField] private TMP_Text _rewarCount;
    [SerializeField] private TMP_Text _decription;
    [SerializeField] private TMP_Text _counter;
    [SerializeField] private Image _progressImage;
    [SerializeField] private CanvasGroup _complitedPanel;

    private DailyTask _task;

    public void Initialize(DailyTask task)
    {
        _task = task;
        _task.ChangedCounter += OnChangedCount;
        _task.Completed += OnComlitedCount;

        _rewardIcon.sprite = _task.Reward.Icon;
        _rewarCount.text = _task.Reward.Count.ToString();
        _decription.text = _task.Description;
        _counter.text = $"{_task.CurrentCount}/{_task.TargerCount}";
        _progressImage.fillAmount = 0;
        _complitedPanel.Deactivate();
    }

    private void OnDisable()
    {
      //  _task.ChangedCounter -= OnChangedCount;
     //   _task.Completed -= OnComlitedCount;
    }

    private void OnComlitedCount()
    {
        _complitedPanel.Activate();
    }

    private void OnChangedCount(float targerCount, float currentCount)
    {
        _counter.text = $"{Mathf.Round(currentCount)}/{Mathf.Round(targerCount)}";
        _progressImage.fillAmount = currentCount / targerCount;
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardCard : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _count;

    public void Initialize(TaskReward reward)
    {
        _icon.sprite = reward.Icon;
        _count.text = reward.Count.ToString();
    }
}

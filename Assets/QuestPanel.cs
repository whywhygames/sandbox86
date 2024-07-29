using System.Collections;
using TMPro;
using UnityEngine;

public class QuestPanel : MonoBehaviour
{
    [SerializeField] private Transform _rewardContainer;
    [SerializeField] private TMP_Text _decription;
    [SerializeField] private RewardCard _rewardCardPrefab;
    [SerializeField] private CanvasGroup _thisPanel;
    
    private Quest _targetQuest;

    private void OnEnable()
    {
        _targetQuest = GetComponentInParent<Quest>();  
        _targetQuest.Show += Show;
        _targetQuest.Hide += Hide;
    }

    private void Start()
    {
        _decription.text = _targetQuest.Description;

        foreach (var rewrad in _targetQuest.Rewards)
        {
            RewardCard newRewardCard = Instantiate(_rewardCardPrefab, _rewardContainer);
            newRewardCard.Initialize(rewrad);
        }
    }

    private void OnDisable()
    {
        _targetQuest.Show -= Show;
        _targetQuest.Hide -= Hide;
    }

    private void Show()
    {
        StartCoroutine(Open());
    }

    private void Hide()
    {
        StartCoroutine(Close());
    }

    private IEnumerator Open()
    {
        float elapsedTime = 0;
        float timeAttenuation = 0.5f;
        _thisPanel.interactable = true;
        _thisPanel.blocksRaycasts = true;
        float startAlpha = _thisPanel.alpha;

        while (elapsedTime < timeAttenuation)
        {
            elapsedTime += Time.deltaTime;

            _thisPanel.alpha = Mathf.Lerp(startAlpha, 1, elapsedTime / timeAttenuation);

            yield return null;
        }
    }

    private IEnumerator Close()
    {
        float elapsedTime = 0;
        float timeAttenuation = 0.5f;
        _thisPanel.interactable = false;
        _thisPanel.blocksRaycasts = false;
        float startAlpha = _thisPanel.alpha;

        while (elapsedTime < timeAttenuation)
        {
            elapsedTime += Time.deltaTime;

            _thisPanel.alpha = Mathf.Lerp(startAlpha, 0, elapsedTime / timeAttenuation);

            yield return null;
        }
    }
}

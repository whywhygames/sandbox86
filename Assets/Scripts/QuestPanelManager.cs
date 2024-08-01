using System.Collections;
using TMPro;
using UnityEngine;

public class QuestPanelManager : MonoBehaviour
{
    [Header("Panels:")]
    [SerializeField] private CanvasGroup _complitedPanel;
    [SerializeField] private CanvasGroup _progressPanel;
    [SerializeField] private TMP_Text _counter;

    private CanvasGroup _currentOpenPanel;

    [Header("Parametrs:")]
    [SerializeField] private float _timeAttenuation;

    private Quest _targetQuest;

    public void Initialize(Quest quest)
    {
        _targetQuest = quest;
        StartCoroutine(OpenController(_progressPanel));
        _targetQuest.Completed += Complited;
        _targetQuest.ChangedCounter += ChangedCounter;
    }

    public void Setup()
    {
        StartCoroutine(Close(_progressPanel));
    }

    private void OnDisable()
    {
        _targetQuest.Completed -= Complited;
        _targetQuest.ChangedCounter -= ChangedCounter;
    }

    private void ChangedCounter(float targetCount, float currentCount)
    {
        _counter.text = $"{currentCount} / {targetCount}";
    }

    private void Complited()
    {
        StartCoroutine(OpenController(_complitedPanel));
        StartCoroutine(CloseComplited());
        _targetQuest = null;
    }

    private IEnumerator CloseComplited()
    {
        yield return new WaitForSeconds(3.5f);
        StartCoroutine(Close(_complitedPanel));
    }

    private IEnumerator OpenController(CanvasGroup targetPanel)
    {
        if (_currentOpenPanel != null)
        {
            StartCoroutine(Close(_currentOpenPanel));
            yield return new WaitForSeconds(_timeAttenuation);
        }
         
        StartCoroutine(Open(targetPanel)); 
    }

    private IEnumerator Open(CanvasGroup targetPanel)
    {
        float elapsedTime = 0;
        _currentOpenPanel = targetPanel;
        _currentOpenPanel.interactable = true;
        _currentOpenPanel.blocksRaycasts = true;
        float startAlpha = targetPanel.alpha;

        while (elapsedTime < _timeAttenuation)
        {
            elapsedTime += Time.deltaTime;

            targetPanel.alpha = Mathf.Lerp(startAlpha, 1, elapsedTime / _timeAttenuation);

            yield return null;
        }
    }

    private IEnumerator Close(CanvasGroup targetPanel)
    {
        float elapsedTime = 0;
        _currentOpenPanel.interactable = false;
        _currentOpenPanel.blocksRaycasts = false;
        _currentOpenPanel = null;
        float startAlpha = targetPanel.alpha;

        while (elapsedTime < _timeAttenuation)
        {
            elapsedTime += Time.deltaTime;

            targetPanel.alpha = Mathf.Lerp(startAlpha, 0, elapsedTime / _timeAttenuation);

            yield return null;
        }
    }
}

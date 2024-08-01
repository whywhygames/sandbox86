using System.Collections;
using UnityEngine;

public class TriggerObjectPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup _thisPanel;

    [SerializeField] private TriggerObject _triggerObject;

    private void OnEnable()
    {
        _triggerObject.Show += Show;
        _triggerObject.Hide += Hide;
    }

    private void OnDisable()
    {
        _triggerObject.Show -= Show;
        _triggerObject.Hide -= Hide;
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

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FreeMedecine : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private float _delay;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private Button _activateButton;
    [SerializeField] private TMP_Text _timerText;

    private float _elapdsedTime;
    private bool _active;

    private void OnEnable()
    {
        _activateButton.onClick.AddListener(TryGiveHealth);
    }

    private void OnDisable()
    {
        _activateButton.onClick.RemoveListener(TryGiveHealth);
    }

    private void Update()
    {
        if (_elapdsedTime > 0)
        {
            _elapdsedTime -= Time.deltaTime;

            if (Mathf.Round(_elapdsedTime) < 10)
                _timerText.text = $"{Mathf.Round(_elapdsedTime / 60)}:0{Mathf.Round(_elapdsedTime % 60)}";
            else
                _timerText.text = $"{Mathf.Round(_elapdsedTime / 60) - 1}:{Mathf.Round(_elapdsedTime % 60)}";
        }
        else if (_active == false)
        {
            _active = true;
            _activateButton.GetComponent<CanvasGroup>().Activate();
            _timerText.GetComponent<CanvasGroup>().Deactivate();
        }
    }

    private void TryGiveHealth()
    {
        if (_elapdsedTime <= 0)
        {
            _playerHealth.AddHealth(_health);
            _activateButton.GetComponent<CanvasGroup>().Deactivate();
            _timerText.GetComponent<CanvasGroup>().Activate();
            _elapdsedTime = _delay;
            _active = false;
        }
    }
}

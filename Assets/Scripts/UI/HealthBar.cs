using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _playerHealth.ChangeHealth += ChangeText;
    }

    private void OnDisable()
    {
        _playerHealth.ChangeHealth -= ChangeText;
    }

    private void ChangeText(int healthCount)
    {
        _slider.maxValue = _playerHealth.MaxHealth;
        _slider.value = healthCount;
        _text.text = $"Health: {healthCount}";
    }
}

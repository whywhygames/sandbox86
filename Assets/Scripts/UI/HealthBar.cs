using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _bar;

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
        _bar.fillAmount = (float)healthCount / _playerHealth.MaxHealth;
        _text.text = $"{healthCount}";
    }
}

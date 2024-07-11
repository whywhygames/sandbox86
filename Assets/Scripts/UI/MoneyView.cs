using TMPro;
using UnityEngine;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private PlayerMoney _playerMoney;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _playerMoney.ChangeMoney += ChangeText;
    }

    private void OnDisable()
    {
        _playerMoney.ChangeMoney -= ChangeText;
    }

    private void ChangeText(int moneyCount)
    {
        _text.text = $"Money: {moneyCount}";
    }
}

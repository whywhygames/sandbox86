using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    [SerializeField] private Image _lockBacground;
    [SerializeField] private Button _button;

    public void AddListener(UnityAction action)
    {
        _button.onClick.AddListener(action);
    }

    public void RemoveListener(UnityAction action)
    {
        _button.onClick.RemoveListener(action);
    }

    public void UpdateLockBacground(bool isActive)
    {
        _lockBacground.color = isActive ? new Color(0, 0, 0, 0) : new Color(0, 0, 0, 0.5f);
        _button.interactable = isActive;
    }
}

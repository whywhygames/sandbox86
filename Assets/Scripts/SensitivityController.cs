using System.Collections;
using System.Collections.Generic;
using TouchControlsKit;
using UnityEngine;
using UnityEngine.UI;

public class SensitivityController : MonoBehaviour
{
    [SerializeField] private TCKTouchpad _touchpad;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Slider _slider;
    [SerializeField] private CanvasGroup _thisPanel;
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;

    private void OnEnable()
    {
        _saveButton.onClick.AddListener(Save);
        _openButton.onClick.AddListener(Open);
        _closeButton.onClick.AddListener(Close);
    }

    private void Start()
    {
        if (PlayerPrefs.GetFloat("Sensitivity") < 0.5)
        {
            PlayerPrefs.SetFloat("Sensitivity", 1);
        }

        _touchpad.sensitivity = PlayerPrefs.GetFloat("Sensitivity");
    }

    private void OnDisable()
    {
        _saveButton.onClick.RemoveListener(Save);
        _openButton.onClick.RemoveListener(Open);
        _closeButton.onClick.RemoveListener(Close);
    }

    private void Open()
    {
        _thisPanel.Activate();
        _slider.value = PlayerPrefs.GetFloat("Sensitivity");
    }

    private void Close()
    {
        _thisPanel.Deactivate();
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("Sensitivity", _slider.value);
        _touchpad.sensitivity = _slider.value;
    }
}

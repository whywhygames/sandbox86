using CoverShooter;
using TMPro;
using UnityEngine;

public class GrenadeCountView : MonoBehaviour
{
    [SerializeField] private GrenadeInventoryCounter _grenadeInventoryCounter;
    [SerializeField] private TMP_Text _textCounter;

    public void OnEnable()
    {
        _grenadeInventoryCounter.ChangeCount += UpdateCounter;
        _textCounter.text = _grenadeInventoryCounter.CountGrenade.ToString();
    }

    private void OnDisable()
    {
        _grenadeInventoryCounter.ChangeCount -= UpdateCounter;
    }

    private void UpdateCounter()
    {
        _textCounter.text = _grenadeInventoryCounter.CountGrenade.ToString();
    }
}

using TMPro;
using UnityEngine;

public class MineCounterView : MonoBehaviour
{
    [SerializeField] private MineSpawner _mineSpawner;
    [SerializeField] private TMP_Text _counterText;

    private void OnEnable()
    {
        _mineSpawner.MineSpawned += ChangeText;
        _counterText.text = _mineSpawner.MineCount.ToString();
    }

    private void OnDisable()
    {
        _mineSpawner.MineSpawned -= ChangeText;
    }

    private void ChangeText()
    {
        _counterText.text = _mineSpawner.MineCount.ToString();
    }
}

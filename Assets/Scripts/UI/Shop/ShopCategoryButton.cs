using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopCategoryButton : MonoBehaviour
{
    [SerializeField] private ShopCategoty _categoty;
    [SerializeField] private Shop _shop;

    private Button _button;

    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ShowPanel);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ShowPanel);
    }

    private void ShowPanel()
    {
        _shop.SetPanel(_categoty);
    }
}

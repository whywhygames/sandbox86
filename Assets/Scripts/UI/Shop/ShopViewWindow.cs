using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopViewWindow : MonoBehaviour 
{
    [SerializeField] private Image _fullImage;
    [SerializeField] private TMP_Text _lable;
    [SerializeField] private TMP_Text _price;
    public void Initialize(ShopItemConfigure shopItemConfigure)
    {
        _fullImage.sprite = shopItemConfigure.FullImage;
        _lable.text = shopItemConfigure.Lable;
        _price.text = shopItemConfigure.Price.ToString();
    }
}
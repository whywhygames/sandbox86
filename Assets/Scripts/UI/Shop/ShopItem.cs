using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private ShopViewWindow _viewWindow;
    [SerializeField] private Shop _shop;
    [SerializeField] private ShopItemConfigure _configure;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private TMP_Text _count;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _outline;

    public ShopItemConfigure Configure { get => _configure; private set => _configure = value; }

    private void OnEnable()
    {
        _button.onClick.AddListener(Show);
    }

    private void Start()
    {
        _icon.sprite = _configure.Icon;
        _price.text = _configure.Price.ToString();
        _count.text = "x"+_configure.Count.ToString();
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Show);
    }

    public void Show()
    {
        _shop.SetItem(this);
        _outline.SetActive(true);
    }

    public void DisableOutline()
    {
        _outline.SetActive(false);
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemConfigure", menuName = "Create new ShopItem", order = 51)]
public class ShopItemConfigure : ScriptableObject
{
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public Sprite FullImage { get; private set; }
    [field: SerializeField] public string Lable { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public int Count { get; private set; }
    [field: SerializeField] public RewardType Rewardtype { get; private set; }
}
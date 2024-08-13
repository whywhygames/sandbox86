using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterButton", menuName = "CharacterView", order = 51)]

public class CharacterViewConfigure : ScriptableObject
{
    [field: SerializeField] public CharacterType CharacterType { get; private set; }
    [field: SerializeField] public Sprite Icon;
}

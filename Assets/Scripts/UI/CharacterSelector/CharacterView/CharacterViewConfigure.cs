using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterButton", menuName = "CharacterView", order = 51)]

public class CharacterViewConfigure : ScriptableObject
{
    [field: SerializeField] public CharacterType CharacterType { get; private set; }
    [field: SerializeField] public Sprite IconButton;
    [field: SerializeField] public Sprite IconCharacter;
}

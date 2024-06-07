using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Characters", order = 51)]

public class CharacterConfigure : ScriptableObject
{
    [field: SerializeField] public CharacterType CharacterType { get; private set; }
    [field: SerializeField] public Mesh Mesh;
    [field: SerializeField] public Texture Texture;

}

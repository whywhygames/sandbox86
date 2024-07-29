using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Characters", order = 51)]

public class CharacterConfigure : ScriptableObject
{
    [field: SerializeField] public CharacterType CharacterType { get; private set; }
    [field: SerializeField] public Mesh Mesh;
    [field: SerializeField] public Texture Texture;
    [field: SerializeField] public RuntimeAnimatorController Animator;
    [field: SerializeField] public float JumpSpeed;
    [field: SerializeField] public float FistPower;
    [field: SerializeField] public float BaseBloom;
    [field: SerializeField] public float MaxBloom;
  //  [field: SerializeField] public AnimatorController Animator;

}

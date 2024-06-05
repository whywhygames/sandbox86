using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class CharacterVariant : MonoBehaviour
{
    [field: SerializeField] public CharacterType CharacterType { get; private set; }
    [field: SerializeField] public ThirdPersonCharacter ThirdPersonCharacter { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
    [field: SerializeField] public CapsuleCollider Collider { get; private set; }

    public void Activate(Transform newTransform)
    {
        gameObject.SetActive(true);
        ThirdPersonCharacter.Initialize();
        transform.position = newTransform.position;
        transform.rotation = newTransform.rotation;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

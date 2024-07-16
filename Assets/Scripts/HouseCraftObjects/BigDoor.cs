using UnityEngine;

public class BigDoor : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void OnTriggerEnter(Collider other)
    {
        _animator.SetBool("IsTrigger", true);
    }

    private void OnTriggerExit(Collider other)
    {
        _animator.SetBool("IsTrigger", false);
    }
}

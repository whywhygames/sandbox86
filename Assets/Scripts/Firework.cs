using UnityEngine;

public class Firework : MonoBehaviour
{
    [SerializeField] private float _destroyDelay = 20;

    private void Start()
    {
        Destroy(gameObject, _destroyDelay);
    }
}

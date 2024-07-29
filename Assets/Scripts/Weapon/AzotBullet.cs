using UnityEngine;

public class AzotBullet : MonoBehaviour
{
    [SerializeField] private AzotFog _azotFog;
    [SerializeField] private bool _useFog;

    private void OnTriggerEnter(Collider other)
    {
        if (_useFog == false)
        {
            Instantiate(_azotFog, transform.position, Quaternion.identity);
            _useFog = true;
            Destroy(gameObject);
        }
    }

   /* private void OnCollisionEnter(Collision other)
    {
        if (_useFog == false)
        {
            Instantiate(_azotFog, transform.position, Quaternion.identity);
            _useFog = true;
        }
    }*/
}
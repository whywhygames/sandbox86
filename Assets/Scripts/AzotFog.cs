using System.Collections.Generic;
using UnityEngine;

public class AzotFog : MonoBehaviour
{
    [SerializeField] private float _time;
    [SerializeField] private List<FreezController> _freezObject = new List<FreezController>();

    private void Update()
    {
        _time -= Time.deltaTime;

        if (_time <= 0)
        {
            foreach (FreezController controller in _freezObject)
                controller.Defreez();

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out FreezController controller))
        {
            _freezObject.Add(controller);
            controller.Freez();
        }
    }
}

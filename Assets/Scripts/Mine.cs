using CoverShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class Mine : MonoBehaviour
{
    [SerializeField] private float _power;
    [SerializeField] private ParticleSystem _boomPaticle;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _radius;
    [SerializeField] private float _radiusAttack;

    private void Update()
    {
        Collider[] collision = Physics.OverlapSphere(transform.position, _radius, _layerMask);

        if (collision.Length > 0)
        {
            Collider[] hitAttack = Physics.OverlapSphere(transform.position, _radiusAttack, _layerMask);
            var particle = Instantiate(_boomPaticle, transform.position, Quaternion.identity);
            particle.gameObject.SetActive(true);

            foreach (Collider c in hitAttack)
            {
                c.GetComponent<EnemyHealth>().TakeDamage(_power);
            }

            Destroy(gameObject);
        }
    }

   /* private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out PlayerHealth player))
            return;

        Debug.Log(1234);
        Collider[] hitAttack = Physics.OverlapSphere(transform.position, _radiusAttack);
        var particle = Instantiate(_boomPaticle, transform.position, Quaternion.identity);
        particle.gameObject.SetActive(true);

        foreach (Collider c in hitAttack)
        {
            c.GetComponent<EnemyHealth>().TakeDamage(_power);
        }

        Destroy(gameObject);
    }*/

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.3f);
        Gizmos.DrawSphere(transform.position, _radius);
    }
}

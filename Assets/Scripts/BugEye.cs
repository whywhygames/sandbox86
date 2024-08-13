using CoverShooter;
using UnityEngine;

public class BugEye : MonoBehaviour
{
    [SerializeField] private EnemyHealth _enemyHealth;

    public void OnHit(Hit hit)
    {
        _enemyHealth.OnHit(hit);
    }
}

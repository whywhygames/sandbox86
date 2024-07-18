using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<EnemyField> _enemyFields = new List<EnemyField>();
    
    private List<EnemyHealth> _allEnemies = new List<EnemyHealth>();

    public List<EnemyHealth> AllEnemies { get => _allEnemies; private set => _allEnemies = value; }

    private void Start()
    {
        foreach (EnemyField enemyField in _enemyFields)
        {
            foreach (EnemyHealth enemy in enemyField.Enemies)
            {
                _allEnemies.Add(enemy);
            }
        }
    }
}

[Serializable]
public class EnemyField
{
    [SerializeField] private Transform _point;

    public List<EnemyHealth> Enemies = new List<EnemyHealth>();
}

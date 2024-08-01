using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private QuestManager _questManager;
    [SerializeField] private SupportManager _supportManager;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private GameState _gameState;

    private void OnEnable()
    {
        _playerHealth.HalfHealth += OnHalfHealthForPlayer;
        _playerHealth.Died += OnDied;
    }

    private void OnDisable()
    {
        _playerHealth.HalfHealth -= OnHalfHealthForPlayer;
        _playerHealth.Died -= OnDied;
    }

    private void OnDied()
    {
        _enemyManager.Setup();
        _supportManager.Setup();
        _questManager.Setup();
    }

    private void OnHalfHealthForPlayer()
    {
        
    }
}

public enum GameState
{
    Default,
    Fight,
    Boss
}
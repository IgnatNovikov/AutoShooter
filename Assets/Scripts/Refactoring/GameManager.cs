using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int enemyMask { get; private set; }
    public int playerMask { get; private set; }
    public State state;

    private GameState _gameState;

    [Header("References")]
    [SerializeField] private PlayerController _player;
    [SerializeField] private EnemyPool _spawner;
    [SerializeField] private UIController _ui;
    [SerializeField] private string _enemyLayer;
    [SerializeField] private string _playerLayer;

    private enum GameState
    {
        PLAY,
        PAUSE
    }

    public PlayerController Player
    {
        get => _player;
    }

    public EnemyPool Spawner
    {
        get => _spawner;
    }

    public UIController UI
    {
        get => _ui;
    }

    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        state = new StatePlay();
        enemyMask = 1 << LayerMask.NameToLayer(_enemyLayer);
        playerMask = 1 << LayerMask.NameToLayer(_playerLayer);
    }

    public void RestartGame()
    {
        _spawner.Restart();
        _player.Restart();
        state.Next();
    }

    public void Defeat()
    {
        _ui.Defeat();
        state.Next();
    }

    public void Victory()
    {
        _ui.Victory();
        state.Next();
    }
}

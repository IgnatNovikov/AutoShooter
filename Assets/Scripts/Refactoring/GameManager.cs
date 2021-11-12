using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private PlayerController _player;
    [SerializeField] private SpawnerController _spawner;
    [SerializeField] private UIController _ui;

    public PlayerController Player
    {
        get => _player;
    }

    public SpawnerController Spawner
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
        StartGame();
    }

    void StartGame()
    {

    }

    void RestartGame()
    {
        _spawner.Restart();
        //_player.Restart();
    }
}

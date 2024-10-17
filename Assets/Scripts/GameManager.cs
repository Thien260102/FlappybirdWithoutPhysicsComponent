using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Bird _player;
    [SerializeField] List<BaseGameObject> _gameObjects;
    [SerializeField] UIManager _uiManager;

    public static GameManager Instance { get; private set; }

    public Action StartGame;
    public Action PauseGame;
    public Action ContinueGame;
    public Action<int> UpdateScore;
    public Action<int> GameOver;
    public Action RestartGame;

    private CollisionHandler _collisionHandler;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _collisionHandler = new CollisionHandler();

        _player.OnAddEvent();
        _player.Initialize();
        foreach (var g in _gameObjects)
        {
            g.OnAddEvent();
            g.Initialize();
        }

        _uiManager.OpenUI(UIType.Menu);
    }

    private void Update()
    {
        _player.UpdateGameObject();
        foreach (var g in _gameObjects)
        {
            g.UpdateGameObject();
        }

        HandleCollision();
    }

    private void HandleCollision()
    {
        _collisionHandler.CheckCollision(_player, _gameObjects);

        for (int i = 0; i < _gameObjects.Count; i++)
        {

        }
    }

    public void Play()
    {
        _player.Initialize();

        _uiManager.OpenUI(UIType.InGame);
        StartGame?.Invoke();
    }

    public void Pause()
    {
        PauseGame?.Invoke();

        _uiManager.OpenUI(UIType.Pause);
    }

    public void Continue()
    {
        ContinueGame?.Invoke();

        _uiManager.OpenUI(UIType.InGame);
    }

    public void BirdDeath(int score)
    {
        Pause();

        _uiManager.OpenUI(UIType.GameOver);
        GameOver?.Invoke(score);
    }

    public void UpdateScoreUI(int score)
    {
        UpdateScore?.Invoke(score);
    }

    private void OnApplicationQuit()
    {
        _player.OnRemoveEvent();
        foreach (var g in _gameObjects)
        {
            g.OnRemoveEvent();
        }
    }
}

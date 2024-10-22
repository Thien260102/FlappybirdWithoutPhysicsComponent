using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Bird _player;
    [SerializeField] PipeController _pipeController;
    [SerializeField] List<BaseGameObject> _gameObjects;
    [SerializeField] UIManager _uiManager;

    public static GameManager Instance { get; private set; }

    public UIManager UIManager => _uiManager;

    public Action StartGame;
    public Action PauseGame;
    public Action ContinueGame;
    public Action<int> UpdateScore;
    public Action<int> GameOver;
    public Action RestartGame;

    private CollisionHandler _collisionHandler;

    private bool _isPausing;

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
        _isPausing = false;
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
        if (_isPausing)
        {
            return;
        }

        _player.UpdateGameObject(Time.deltaTime);
        foreach (var g in _gameObjects)
        {
            g.UpdateGameObject(Time.deltaTime);
        }

        _pipeController.UpdatePipesController(Time.deltaTime);

        HandleCollision();
    }

    private void HandleCollision()
    {
        _collisionHandler.CheckCollision(_player, _gameObjects);

        for (int i = 0; i < _gameObjects.Count; i++)
        {
            _collisionHandler.CheckCollision(_gameObjects[i], 
                _gameObjects.Where((baseObject, index) => index != i).ToList());
        }
    }

    public void Play(bool isAutoPlay = false)
    {
        _isPausing = false;
        _player.Initialize();
        _player.IsAutoPlay = isAutoPlay;

        _uiManager.OpenUI(UIType.InGame);
        StartGame?.Invoke();
    }

    public void Restart()
    {
        _isPausing = false;
        _player.Initialize();

        _uiManager.OpenUI(UIType.InGame);
        StartGame?.Invoke();
    }

    public void Pause()
    {
        PauseGame?.Invoke();
        _isPausing = true;

        _uiManager.OpenUI(UIType.Pause);
    }

    public void Continue()
    {
        _isPausing = false;
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Bird _player;
    [SerializeField] UIManager _uiManager;

    public static GameManager Instance { get; private set; }

    public Action StartGame;
    public Action PauseGame;
    public Action ContinueGame;
    public Action<int> UpdateScore;
    public Action GameOver;
    public Action RestartGame;

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
        _player.Initialize();
        _uiManager.OpenUI(UIType.Menu);
    }

    public void Play()
    {
        StartGame?.Invoke();

        _uiManager.OpenUI(UIType.InGame);
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

    public void BirdDeath()
    {
        Pause();

        GameOver?.Invoke();
        _uiManager.OpenUI(UIType.GameOver);
    }

    public void UpdateScoreUI(int score)
    {
        UpdateScore?.Invoke(score);
    }
}

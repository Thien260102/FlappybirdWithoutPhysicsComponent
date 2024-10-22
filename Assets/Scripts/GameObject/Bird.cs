using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : BaseGameObject
{
    [SerializeField] BirdTrigger[] _flyTriggers;
    [SerializeField] bool _isAutoPlay = true;
    [SerializeField] float _strength = 2.5f;
    [SerializeField] float _gravity = -9.81f;
    [SerializeField] float _rotateSpeed = 5f;

    private Vector3 _direction;
    private bool _isPausing;
    private int _score;

    public bool IsAutoPlay
    {
        get => _isAutoPlay;
        set
        {
            _isAutoPlay = value;
            if (_isAutoPlay)
            {
                foreach (var flyTrigger in _flyTriggers)
                {
                    flyTrigger.OnBirdFly += BirdFly;
                }
            }
            else
            {
                foreach (var flyTrigger in _flyTriggers)
                {
                    flyTrigger.OnBirdFly -= BirdFly;
                }
            }
        }
    }

    public override void OnAddEvent()
    {
        base.OnAddEvent();

        GameManager.Instance.StartGame += StartGame;
        GameManager.Instance.PauseGame += PauseGame;
        GameManager.Instance.ContinueGame += StartGame;
    }

    public override void OnRemoveEvent()
    {
        base.OnRemoveEvent();

        GameManager.Instance.StartGame -= StartGame;
        GameManager.Instance.PauseGame -= PauseGame;
        GameManager.Instance.ContinueGame -= StartGame;
    }

    public override void Initialize()
    {
        base.Initialize();
        _type = ObjectType.Bird;

        _score = 0;
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        _direction = Vector3.zero;

        _isPausing = true;
    }

    private void StartGame()
    {
        _isPausing = false;
    }

    private void PauseGame()
    {
        _isPausing = true;
    }

    private void GetScore()
    {
        _score++;
        GameManager.Instance.UpdateScore?.Invoke(_score);
    }

    private void BirdFly(Pipe pipe = null)
    {
        if (_isPausing)
        {
            return;
        }

        _direction = Vector3.up * _strength;
    }

    public override void UpdateGameObject(float time)
    {
        base.UpdateGameObject(time);

        if (_isPausing)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            BirdFly();
        }

        // Apply gravity and update the position
        _direction.y += _gravity * time;
        transform.position += _direction * time;

        // Tilt the bird based on the direction
        Vector3 rotation = transform.eulerAngles;
        rotation.z = _direction.y * _rotateSpeed;
        transform.eulerAngles = rotation;
    }

    public override void OnCustomTrigger(BaseGameObject baseGameObject)
    {
        base.OnCustomTrigger(baseGameObject);

        switch (baseGameObject.Type)
        {
            case ObjectType.Ground:
                GameManager.Instance.BirdDeath(_score);
                break;

            case ObjectType.Pipe:
                GameManager.Instance.BirdDeath(_score);
                break;

            case ObjectType.Score:
                baseGameObject.IsCollidable = false;
                GetScore();
                break;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : BaseGameObject
{
    [SerializeField] float _strength = 1f;
    [SerializeField] float _gravity = -9.81f;
    [SerializeField] float _rotateSpeed = 5f;

    private Vector3 _direction;
    private bool _isPausing;
    private int _score;

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

    public override void UpdateGameObject()
    {
        base.UpdateGameObject();

        if (_isPausing)
        {
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            GameManager.Instance.BirdDeath(_score);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            GetScore();
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            _direction = Vector3.up * _strength;
        }

        // Apply gravity and update the position
        _direction.y += _gravity * Time.deltaTime;
        transform.position += _direction * Time.deltaTime;

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

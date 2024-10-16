using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float strength = 5f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float tilt = 5f;

    private Vector3 _direction;

    private bool _isPausing;

    private int _score;

    public void Initialize()
    {
        _score = 0;
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        _direction = Vector3.zero;

        _isPausing = true;

        GameManager.Instance.StartGame += StartGame;
        GameManager.Instance.PauseGame += PauseGame;
        GameManager.Instance.ContinueGame += StartGame;
    }

    private void StartGame()
    {
        _isPausing = false;
    }

    private void PauseGame()
    {
        _isPausing = true;
    }

    private void Update()
    {
        if (_isPausing)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            _direction = Vector3.up * strength;
        }

        // Apply gravity and update the position
        _direction.y += gravity * Time.deltaTime;
        transform.position += _direction * Time.deltaTime;

        // Tilt the bird based on the direction
        Vector3 rotation = transform.eulerAngles;
        rotation.z = _direction.y * tilt;
        transform.eulerAngles = rotation;
    }

    private void OnDisable()
    {
        GameManager.Instance.StartGame -= StartGame;
        GameManager.Instance.PauseGame -= PauseGame;
        GameManager.Instance.ContinueGame -= StartGame;
    }
}

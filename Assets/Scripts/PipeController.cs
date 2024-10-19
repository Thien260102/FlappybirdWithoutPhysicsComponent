using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    [SerializeField] GameObject[] _pairPipes;
    [SerializeField] CustomTrigger _customTrigger;
    [SerializeField] float _pipesSpeed = .2f;
    [SerializeField] float _xDistance = .55f;   // distance between ahead pipes and behind pipes
    [SerializeField] float _minYPos = -0.435f;  // min y position that pipes can be placed
    [SerializeField] float _maxYPos = 0.494f;   // max y position that pipes can be placed
    [SerializeField] float _maxDiffY = .2f;     // difference between ahead pipes y and behind pipes y

    private Vector3 _moveDirection;
    private Vector3 _startPipePosition;
    private Vector3 _lastPipesPosition;

    private bool _isPausing = false;

    private void OnEnable()
    {
        GameManager.Instance.StartGame += StartGame;
        GameManager.Instance.PauseGame += () => _isPausing = true;
        GameManager.Instance.ContinueGame += () => _isPausing = false;

        if (_customTrigger)
        {
            _customTrigger.OnPipeCollided += ResetPipes; 
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.StartGame -= StartGame;
        GameManager.Instance.PauseGame -= () => _isPausing = true;
        GameManager.Instance.ContinueGame -= () => _isPausing = false;

        if (_customTrigger)
        {
            _customTrigger.OnPipeCollided -= ResetPipes;
        }
    }

    private void Awake()
    {
        if (_pairPipes.Length == 0)
        {
            return;
        }

        _startPipePosition = _pairPipes[0].transform.position;
    }

    private void StartGame()
    {
        _moveDirection = new Vector3(-1, 0, 0);
        _isPausing = false;

        int length = _pairPipes.Length;
        if (length == 0)
        {
            return;
        }

        Vector3 firstPosition = _startPipePosition;
        for (int i = 0; i < length; i++)
        {
            _pairPipes[i].transform.position = firstPosition;
            firstPosition.x += _xDistance; 
            
            var score = _pairPipes[i].GetComponentInChildren<Score>();
            if (score)
            {
                score.IsCollidable = true;
            }
        }

        _lastPipesPosition = _pairPipes[length - 1].transform.position;
    }

    public void UpdatePipesController(float time)
    {
        if (_isPausing)
        {
            return;
        }

        _lastPipesPosition = _pairPipes[0].transform.position;

        Vector3 moveDistance = _moveDirection * _pipesSpeed * time;
        foreach (var pipes in _pairPipes)
        {
            pipes.transform.position += moveDistance;
            if (pipes.transform.position.x > _lastPipesPosition.x)
            {
                _lastPipesPosition = pipes.transform.position;
            }
        }
    }

    private void ResetPipes(BaseGameObject pipe)
    {
        _lastPipesPosition.y = Mathf.Clamp(_lastPipesPosition.y + Random.Range(-_maxDiffY, _maxDiffY), _minYPos, _maxYPos);
        _lastPipesPosition.x += _xDistance;
        foreach (var pipes in _pairPipes)
        {
            if (pipe.transform.parent == pipes.transform)
            {
                pipes.transform.position = _lastPipesPosition;

                var score = pipes.GetComponentInChildren<Score>();
                if (score)
                {
                    score.IsCollidable = true;
                }
                return;
            }
        }
    }
}

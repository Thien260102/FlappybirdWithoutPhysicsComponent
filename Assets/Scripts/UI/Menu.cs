using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : UIBase
{
    [SerializeField] Button _startButton;
    [SerializeField] Button _autoPlay;

    public override void Open()
    {
        base.Open();

        _startButton.onClick.AddListener(StartGame);
        _autoPlay.onClick.AddListener(AutoPlay);
        Debug.Log("Menu open");
    }

    public override void Close()
    {
        base.Close();

        _startButton.onClick.RemoveListener(StartGame);
        _autoPlay.onClick.RemoveListener(AutoPlay);
    }

    private void StartGame()
    {
        GameManager.Instance.Play();
    }

    private void AutoPlay()
    {
        GameManager.Instance.Play(true);
    }
}

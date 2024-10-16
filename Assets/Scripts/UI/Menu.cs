using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : UIBase
{
    [SerializeField] Button _startButton;

    public override void Open()
    {
        base.Open();

        _startButton.onClick.AddListener(StartGame);
        Debug.Log("Menu open");
    }

    public override void Close()
    {
        base.Close();

        _startButton.onClick.RemoveListener(StartGame);
    }

    private void StartGame()
    {
        GameManager.Instance.Play();
    }
}

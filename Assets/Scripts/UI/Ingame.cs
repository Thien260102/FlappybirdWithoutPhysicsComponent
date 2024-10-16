using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ingame : UIBase
{
    [SerializeField] Button _pauseButton;
    [SerializeField] TMPro.TMP_Text _scoreText;

    public override void Open()
    {
        base.Open();

        _pauseButton.onClick.AddListener(PauseGame);
        GameManager.Instance.UpdateScore += UpdateScore;
    }

    public override void Close()
    {
        base.Close();

        _pauseButton.onClick.RemoveListener(PauseGame);
        GameManager.Instance.UpdateScore -= UpdateScore;
    }

    private void PauseGame()
    {
        GameManager.Instance.Pause();
    }

    private void UpdateScore(int score) => _scoreText.text = score.ToString();
}

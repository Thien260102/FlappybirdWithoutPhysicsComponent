using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : UIBase
{
    [SerializeField] TMPro.TMP_Text _scoreText;
    [SerializeField] Button _restartButton;

    public override void Open()
    {
        base.Open();

        _restartButton.onClick.AddListener(RestartGame);
    }

    public override void Close()
    {
        base.Close();

        _restartButton.onClick.RemoveListener(RestartGame);
    }

    public void SetScore(int score)
    {
        _scoreText.text = score.ToString();
    }

    private void RestartGame()
    {
        GameManager.Instance.Play();
    }
}

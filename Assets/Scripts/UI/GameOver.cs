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
        GameManager.Instance.GameOver += SetScore;
    }

    public override void Close()
    {
        base.Close();

        _restartButton.onClick.RemoveListener(RestartGame);
        GameManager.Instance.GameOver -= SetScore;
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

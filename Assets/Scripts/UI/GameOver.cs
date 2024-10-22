using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : UIBase
{
    [SerializeField] TMPro.TMP_Text _scoreText;
    [SerializeField] Button _restartButton;
    [SerializeField] Button _menuButton;

    public override void Open()
    {
        base.Open();

        _restartButton.onClick.AddListener(RestartGame);
        _menuButton.onClick.AddListener(OpenMenu);
        GameManager.Instance.GameOver += SetScore;
    }

    public override void Close()
    {
        base.Close();

        _restartButton.onClick.RemoveListener(RestartGame);
        _menuButton.onClick.RemoveListener(OpenMenu);
        GameManager.Instance.GameOver -= SetScore;
    }

    public void SetScore(int score)
    {
        _scoreText.text = score.ToString();
    }

    private void RestartGame()
    {
        GameManager.Instance.Restart();
    }

    private void OpenMenu()
    {
        var uiManager = GameManager.Instance.UIManager;
        uiManager.OpenUI(UIType.Menu);
    }
}

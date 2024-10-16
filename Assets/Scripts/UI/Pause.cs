using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Pause : UIBase
{
    [SerializeField] Button _continueButton;

    public override void Open()
    {
        base.Open();

        _continueButton.onClick.AddListener(ContinueGame);
    }

    public override void Close()
    {
        base.Close();

        _continueButton.onClick.RemoveListener(ContinueGame);
    }

    private void ContinueGame()
    {
        GameManager.Instance.Continue();
    }
}

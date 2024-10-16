using System.Collections;
using UnityEngine;

public enum UIType
{
    Menu,
    InGame,
    Pause,
    GameOver
}

public class UIBase : MonoBehaviour
{
    [SerializeField] protected UIType _type;

    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
}

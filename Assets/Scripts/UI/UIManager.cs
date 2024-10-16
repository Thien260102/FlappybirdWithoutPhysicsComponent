using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UIData
{
    [SerializeField] public UIType Type;
    [SerializeField] public UIBase Base;
}


public class UIManager : MonoBehaviour
{
    [SerializeField] List<UIData> _myUIs;

    private Dictionary<UIType, UIBase> _dictionaryUIs;

    private void Awake()
    {
        _dictionaryUIs = new Dictionary<UIType, UIBase>();

        foreach (var ui in _myUIs)
        {
            _dictionaryUIs.Add(ui.Type, ui.Base);
            ui.Base.Close();

            Debug.Log($"UI {ui.Type}");
        }
    }

    public void OpenUI(UIType type)
    {
        foreach (var ui in _dictionaryUIs)
        {
            ui.Value.Close();
        }

        _dictionaryUIs[type].Open();
    }
}

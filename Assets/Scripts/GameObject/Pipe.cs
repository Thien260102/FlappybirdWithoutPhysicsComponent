using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : BaseGameObject
{
    public override void Initialize()
    {
        base.Initialize();
        _type = ObjectType.Pipe;
    }

    public override void UpdateGameObject(float time)
    {
        base.UpdateGameObject(time);
    }
}

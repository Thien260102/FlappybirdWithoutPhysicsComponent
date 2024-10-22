using System.Collections;
using UnityEngine;


public class Ground : BaseGameObject
{
    public override void Initialize()
    {
        base.Initialize();
        _type = ObjectType.Ground;
    }

    public override void UpdateGameObject(float time)
    {
        base.UpdateGameObject(time);
    }
}

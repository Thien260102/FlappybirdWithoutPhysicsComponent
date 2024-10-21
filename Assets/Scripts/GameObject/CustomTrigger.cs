using System.Collections;
using UnityEngine;

public class CustomTrigger : BaseGameObject
{
    public System.Action<Pipe> OnPipeCollided;

    public override void Initialize()
    {
        base.Initialize();

        _type = ObjectType.Trigger;
    }

    public override void OnCustomTrigger(BaseGameObject baseGameObject)
    {
        base.OnCustomTrigger(baseGameObject);

        Pipe pipe = baseGameObject as Pipe;
        if (pipe == null || pipe.transform.lossyScale.y < 0)
        {
            return;
        }

        OnPipeCollided?.Invoke(baseGameObject as Pipe);
    }

}

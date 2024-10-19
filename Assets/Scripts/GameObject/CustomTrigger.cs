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

        switch(baseGameObject.Type)
        {
            case ObjectType.Pipe:
                OnPipeCollided?.Invoke(baseGameObject as Pipe);
                break;
        }
    }

}

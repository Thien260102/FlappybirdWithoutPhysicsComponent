using System.Collections;
using UnityEngine;


public class BirdTrigger : BaseGameObject
{
    [SerializeField] float _resizeBox = 2f;

    public System.Action<Pipe> OnBirdFly;

    private Vector3 _originalAngle;

    private bool _isChangedSize;
    private Vector3 _originalSize;

    private void Awake()
    {
        _originalSize = BoxCollider.Size;
        _originalAngle = transform.eulerAngles;
    }

    public override void OnAddEvent()
    {
        base.OnAddEvent();

        GameManager.Instance.StartGame += StartGame;
    }

    public override void OnRemoveEvent()
    {
        base.OnRemoveEvent();

        GameManager.Instance.StartGame -= StartGame;
    }


    public override void Initialize()
    {
        base.Initialize();

        _type = ObjectType.BirdTrigger;
    }

    private void StartGame()
    {
        BoxCollider.Size = _originalSize;
        _isChangedSize = false;
    }

    public override void UpdateGameObject()
    {
        base.UpdateGameObject();

        transform.eulerAngles = _originalAngle;
    }

    public override void OnCustomTrigger(BaseGameObject baseGameObject)
    {
        base.OnCustomTrigger(baseGameObject);

        Pipe pipe = baseGameObject as Pipe;
        if (pipe == null || pipe.transform.lossyScale.y < 0)
        {
            return;
        }
        Debug.Log(gameObject.name);
        OnBirdFly?.Invoke(pipe);

        if (!_isChangedSize)
        {
            var box = BoxCollider;
            box.Size /= _resizeBox;

            _isChangedSize = true;
        }
    }
}

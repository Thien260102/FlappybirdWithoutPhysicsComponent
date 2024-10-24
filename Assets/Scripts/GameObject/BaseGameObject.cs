﻿using System.Collections;
using UnityEngine;

public enum ObjectType
{
    None,
    Bird,
    Ground,
    Pipe,
    Score,
    Trigger,        // use for rearrange pipes
    BirdTrigger,    // use for bird auto dodge pipes
}

[ExecuteInEditMode]
public class BaseGameObject : MonoBehaviour
{
    [SerializeField] Box _boxCollider;

    public Box BoxCollider => _boxCollider;

    public bool IsCollidable = false;

    protected ObjectType _type;
    public ObjectType Type => _type;

    private void Update()
    {
        _boxCollider.Position = transform.position;
    }

    public virtual void OnAddEvent()
    {

    }

    public virtual void OnRemoveEvent()
    {

    }

    public virtual void Initialize()
    {

    }

    public virtual void UpdateGameObject(float time)
    {

    }

    public virtual void OnCustomTrigger(BaseGameObject baseGameObject)
    {

    }

    [ExecuteInEditMode]
    private void OnDrawGizmos()
    {
        if (!_boxCollider.EditCollider)
        {
            return;
        }

        Vector2 position = _boxCollider.Position;
        Vector2 halfSize = _boxCollider.HalfSize;

        Vector2 vertex1 = position - halfSize;
        Vector2 vertex2 = new Vector2(position.x - halfSize.x, position.y + halfSize.y);
        Vector2 vertex3 = new Vector2(position.x + halfSize.x, position.y + halfSize.y);
        Vector2 vertex4 = new Vector2(position.x + halfSize.x, position.y - halfSize.y);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(vertex1, vertex2);
        Gizmos.DrawLine(vertex2, vertex3);
        Gizmos.DrawLine(vertex3, vertex4);
        Gizmos.DrawLine(vertex4, vertex1);
    }
}

using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Box
{
    public bool EditCollider;
    public Vector2 Position;
    public Vector2 Size;

    public Vector2 HalfSize => Size / 2;

    public void GetBounding(out float t, out float b, out float l, out float r)
    {
        t = Position.y + HalfSize.y;
        b = Position.y - HalfSize.y;
        l = Position.x - HalfSize.x;
        r = Position.x + HalfSize.x;
    }
}

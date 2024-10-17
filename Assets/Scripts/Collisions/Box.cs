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
}

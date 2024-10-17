using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollisionHandler
{
    private bool AABB(Box b1, Box b2)
    {
        return b1.Position.x < b2.Position.x + b2.HalfSize.x
            && b1.Position.x + b1.HalfSize.x > b2.Position.x
            && b1.Position.y < b2.Position.y + b2.HalfSize.y
            && b1.Position.y + b1.HalfSize.y > b2.Position.y;
    }

    private List<BaseGameObject> Scan(BaseGameObject source, List<BaseGameObject> gameObjects)
    {
        List<BaseGameObject> collidedObjects = new List<BaseGameObject>();
        
        foreach (var g in gameObjects)
        {
            if (!g.IsCollidable)
            {
                continue;
            }

            if (AABB(source.BoxCollider, g.BoxCollider))
            {
                collidedObjects.Add(g);
            }
        }

        return collidedObjects;
    }

    public void CheckCollision(BaseGameObject sourceObject, List<BaseGameObject> gameObjects)
    {
        List<BaseGameObject> collidedObjects;

        if (sourceObject.IsCollidable)
        {
            collidedObjects = Scan(sourceObject, gameObjects);

            foreach (var collidedObject in collidedObjects)
            {
                sourceObject.OnCustomTrigger(collidedObject);
            }
        }
    }
}

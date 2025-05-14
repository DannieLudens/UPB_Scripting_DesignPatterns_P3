using UnityEngine;

public class CapsuleFactory : IShapeFactory
{
    public GameObject prefab;

    public GameObject CreateShape()
    {
        return GameObject.Instantiate(prefab);
    }
}

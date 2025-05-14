using UnityEngine;

public class SphereFactory : IShapeFactory
{
    public GameObject prefab;

    public GameObject CreateShape()
    {
        return GameObject.Instantiate(prefab);
    }
}

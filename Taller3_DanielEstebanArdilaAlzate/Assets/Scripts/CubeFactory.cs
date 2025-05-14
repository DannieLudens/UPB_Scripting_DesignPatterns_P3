using UnityEngine;

public class CubeFactory : IShapeFactory
{
    public GameObject prefab;

    public GameObject CreateShape()
    {
        return GameObject.Instantiate(prefab);
    }
}

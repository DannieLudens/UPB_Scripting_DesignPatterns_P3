using UnityEngine;

public class ShapeManager : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject spherePrefab;
    public GameObject capsulePrefab;

    private ShapeFacade facade;

    private void Start()
    {
        facade = new ShapeFacade();
        SetCubeFactory(); // Por defecto
    }

    public void SetCubeFactory()
    {
        facade.SetFactory(new CubeFactory { prefab = cubePrefab });
    }

    public void SetSphereFactory()
    {
        facade.SetFactory(new SphereFactory { prefab = spherePrefab });
    }

    public void SetCapsuleFactory()
    {
        facade.SetFactory(new CapsuleFactory { prefab = capsulePrefab });
    }

    public void CreateObject()
    {
        Vector3 spawnPos = GetRandomPositionInView();
        GameObject obj = facade.CreateShape();
        obj.transform.position = spawnPos;
    }

    private Vector3 GetRandomPositionInView()
    {
        // Define un área centrada alrededor del origen
        float x = Random.Range(-4f, 4f); // ancho visible
        float y = Random.Range(-2f, 2f); // alto visible
        return new Vector3(x, y, 0f);    // Z = 0 porque están en vista de cámara
    }

}

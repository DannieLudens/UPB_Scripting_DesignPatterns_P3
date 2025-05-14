using UnityEngine;
using System.Collections.Generic;

public class ProjectilePool : MonoBehaviour
{
    public GameObject prefab;
    private Queue<GameObject> pool = new Queue<GameObject>();

    public GameObject GetObject(Vector3 position)
    {
        GameObject obj = (pool.Count > 0) ? pool.Dequeue() : Instantiate(prefab);
        obj.transform.position = position;
        obj.SetActive(true);
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}

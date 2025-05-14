using UnityEngine;
using System.Collections;

public class ProjectileShooter : MonoBehaviour
{
    public static ProjectileShooter instance;

    public ProjectilePool[] pools;
    public GameObject target;

    private int currentType = 0;
    private bool canShoot = true;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        ProjectileType2.OnFreezeShooter += FreezeShoot;
    }

    private void OnDisable()
    {
        ProjectileType2.OnFreezeShooter -= FreezeShoot;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            Vector3 spawnPos = transform.position;
            Vector3 direction = transform.forward;

            GameObject obj = pools[currentType].GetObject(spawnPos);
            BaseProjectile proj = obj.GetComponent<BaseProjectile>();

            proj.pool = pools[currentType]; // importante para reciclar correctamente

            if (proj is ProjectileType2 p2)
            {
                p2.Initialize(target); // le asignamos el target correctamente
            }

            proj.Activate(direction);
        }

        if (Input.GetMouseButtonDown(1))
        {
            currentType = (currentType + 1) % pools.Length;
            Debug.Log("Tipo de proyectil seleccionado: " + (currentType + 1));
        }
    }

    private void FreezeShoot()
    {
        canShoot = false;
        Invoke(nameof(EnableShoot), 1f);
    }

    private void EnableShoot()
    {
        canShoot = true;
    }

    // Método público y estático que puede ser invocado desde otros scripts
    public static void RequestTargetReset(Collider col)
    {
        if (instance != null && col != null)
        {
            instance.StartCoroutine(instance.ResetTarget(col));
        }
    }

    private IEnumerator ResetTarget(Collider col)
    {
        yield return new WaitForSeconds(1f);

        if (col != null)
        {
            col.enabled = true;
            Debug.Log("Collider reactivado (desde Shooter)");
        }
    }
}

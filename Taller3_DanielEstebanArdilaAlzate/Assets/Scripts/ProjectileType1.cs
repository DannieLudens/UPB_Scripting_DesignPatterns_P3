using UnityEngine;

public class ProjectileType1 : BaseProjectile
{
    public override void Activate(Vector3 direction)
    {
        base.Activate(direction);
        GetComponent<Rigidbody>().linearVelocity = direction * 10f;
    }


    public override void Recycle()
    {
        Debug.Log("Proyectil tipo 1 impactó.");
        base.Recycle();
    }
}

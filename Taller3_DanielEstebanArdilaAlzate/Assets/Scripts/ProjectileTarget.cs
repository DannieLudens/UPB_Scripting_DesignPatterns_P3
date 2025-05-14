using UnityEngine;

public class ProjectileTarget : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        BaseProjectile proj = collision.gameObject.GetComponent<BaseProjectile>();
        if (proj != null)
        {
            proj.Recycle();
        }
    }
}

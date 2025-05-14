using UnityEngine;
using System.Collections;

public class ProjectileType2 : BaseProjectile
{
    public static System.Action OnFreezeShooter;
    public GameObject target; // se asigna en tiempo de ejecuci�n desde ProjectileShooter

    public override void Activate(Vector3 direction)
    {
        base.Activate(direction); // inicia velocidad y reciclaje autom�tico a los 3s
    }

    // Este m�todo se llama desde ProjectileShooter para asignar el Target en runtime
    public void Initialize(GameObject assignedTarget)
    {
        target = assignedTarget;
        if (target != null)
        {
            Debug.Log("Target asignado correctamente: " + target.name);
        }
        else
        {
            Debug.LogWarning("Target NO asignado");
        }
    }

    public override void Recycle()
    {
        // Desactiva temporalmente el Collider del Target
        if (target != null)
        {
            Collider col = target.GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = false;
                Debug.Log("Collider desactivado");
                ProjectileShooter.RequestTargetReset(col); // m�todo est�tico
            }
        }

        // Bloquea disparo desde ProjectileShooter
        OnFreezeShooter?.Invoke();

        // Recicla el proyectil (lo devuelve al pool)
        base.Recycle();
    }

}

using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour
{
    public ProjectilePool pool;

    private float lifetime = 3f;

    public virtual void Activate(Vector3 direction)
    {
        GetComponent<Rigidbody>().linearVelocity = direction * 10f;

        // Inicia temporizador de reciclaje automático
        CancelInvoke(nameof(ForceRecycle));
        Invoke(nameof(ForceRecycle), lifetime);
    }

    public virtual void Recycle()
    {
        CancelInvoke(nameof(ForceRecycle)); // evita doble reciclado
        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        pool.ReturnObject(gameObject);
    }

    private void ForceRecycle()
    {
        Debug.Log("Reciclaje forzado por tiempo");
        Recycle();
    }
}

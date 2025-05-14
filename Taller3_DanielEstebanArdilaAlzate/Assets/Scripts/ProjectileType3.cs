using UnityEngine;

public class ProjectileType3 : BaseProjectile
{
    public GameObject particleEffect;

    public override void Activate(Vector3 direction)
    {
        base.Activate(direction);
        GetComponent<Rigidbody>().linearVelocity = direction * 10f;
    }

    public override void Recycle()
    {
        if (particleEffect != null)
        {
            // Mover el efecto a la posici�n del impacto
            particleEffect.transform.position = transform.position;
            particleEffect.SetActive(true); // Asegura que se muestre

            var ps = particleEffect.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear); // limpia por si acaso
                Debug.Log("Activando part�culas en: " + transform.position);
                ps.Play();

                // Ya no apagamos manualmente aqu�
                // Si usas AutoDisable.cs, �l lo apagar� solo
            }
        }

        base.Recycle(); // Reciclamos el proyectil sin apagar el efecto
    }
}

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
            // Mover el efecto a la posición del impacto
            particleEffect.transform.position = transform.position;
            particleEffect.SetActive(true); // Asegura que se muestre

            var ps = particleEffect.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear); // limpia por si acaso
                Debug.Log("Activando partículas en: " + transform.position);
                ps.Play();

                // Ya no apagamos manualmente aquí
                // Si usas AutoDisable.cs, él lo apagará solo
            }
        }

        base.Recycle(); // Reciclamos el proyectil sin apagar el efecto
    }
}

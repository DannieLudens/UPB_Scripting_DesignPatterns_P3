using UnityEngine;
using System;

public class ClickBroadcaster : MonoBehaviour
{
    public static event Action<int> OnButtonClicked;

    private int counter = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // clic izquierdo
        {
            // Debug.Log("¡Clic izquierdo detectado!");
            counter++;
            int value = (counter - 1) % 4 + 1; // ciclo 1-4
            OnButtonClicked?.Invoke(value);
        }
    }
}

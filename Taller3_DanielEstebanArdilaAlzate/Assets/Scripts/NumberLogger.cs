using UnityEngine;

public class NumberLogger : MonoBehaviour
{
    void OnEnable()
    {
        ClickBroadcaster.OnButtonClicked += LogNumber;
    }

    void OnDisable()
    {
        ClickBroadcaster.OnButtonClicked -= LogNumber;
    }

    void LogNumber(int value)
    {
        Debug.Log("Número actual: " + value);
    }
}

using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        ClickBroadcaster.OnButtonClicked += ChangeColor;
    }

    void OnDestroy()
    {
        ClickBroadcaster.OnButtonClicked -= ChangeColor;
    }

    void ChangeColor(int value)
    {
        switch (value)
        {
            case 1: rend.material.color = Color.red; 
                break;
            case 2: rend.material.color = Color.green; 
                break;
            case 3: rend.material.color = Color.blue; 
                break;
            case 4: rend.material.color = Color.yellow; 
                break;
        }
    }
}

using UnityEngine;
using TMPro;

public class NumberDisplay : MonoBehaviour
{
    public TextMeshProUGUI textElement;

    void OnEnable()
    {
        ClickBroadcaster.OnButtonClicked += UpdateText;
    }

    void OnDisable()
    {
        ClickBroadcaster.OnButtonClicked -= UpdateText;
    }

    void UpdateText(int value)
    {
        textElement.text = "Número actual: " + value;
    }
}

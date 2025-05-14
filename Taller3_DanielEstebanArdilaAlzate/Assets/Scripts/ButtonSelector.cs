using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonSelector : MonoBehaviour
{
    public List<Button> buttons;
    private Color selectedColor = Color.yellow;
    private Color normalColor = Color.white;

    public void SelectButton(Button selected)
    {
        foreach (Button btn in buttons)
        {
            Image image = btn.GetComponent<Image>();
            if (btn == selected)
            {
                image.color = selectedColor;
            }
            else
            {
                image.color = normalColor;
            }
        }
    }
}

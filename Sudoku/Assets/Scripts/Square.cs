using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Square : Selectable
{
    [SerializeField] private TextMeshProUGUI _Text;

    public void HighlightSquare()
    {
        targetGraphic.color = colors.highlightedColor;
    }
    public void ResetSquareColor()
    {
        targetGraphic.color = colors.normalColor;
    }
}

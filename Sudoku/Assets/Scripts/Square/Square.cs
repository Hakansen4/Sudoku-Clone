using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Square : Selectable
{
    [HideInInspector] public int _Rank;
    [SerializeField] private TextMeshProUGUI _Text;
    private int _Number = 0;
    private bool isSolved;
    private bool created;
    private void Update()
    {
        if (IsPressed())
            GameActions.instance._PressedAction.Invoke(_Rank);
    }
    public void HighlightSquare()
    {
        targetGraphic.color = colors.highlightedColor;
    }
    public void ResetSquareColor()
    {
        targetGraphic.color = colors.normalColor;
    }
    public int GetNumber()
    {
        return _Number;
    }
    public void SetNumber(int number)
    {
        _Number = number;
        created = true;
    }
    public bool IsCreated()
    {
        return created;
    }
    public void ResetCreated()
    {
        created = false;
    }
    public void SetAsStarter()
    {
        interactable = false;
        _Text.gameObject.SetActive(true);
        _Text.text = _Number.ToString();
        _Text.color = Color.black;
        isSolved = true;
    }
    public bool CheckSolved()
    {
        return isSolved;
    }
}
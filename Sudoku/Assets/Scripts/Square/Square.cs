using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Square : Selectable
{
    private int _Row, _Collumn;
    [SerializeField] private TextMeshProUGUI _Text;
    [SerializeField] private Color32 _FailedTextColor;
    [SerializeField] private Color32 _CorrectTextColor;
    private int _Number = 0;
    private bool isSolved;
    private bool created;
    private void Update()
    {
        if (IsPressed())
        {
            GameActions.instance._PressedAction?.Invoke(_Row, _Collumn);
            targetGraphic.color = colors.selectedColor;
        }
    }
    public void Play(int Number)
    {
        _Text.gameObject.SetActive(true);
        _Text.text = Number.ToString();
        if (Number == _Number)
        {
            isSolved = true;
            //_Text.color = Color.cyan;
            _Text.color = _CorrectTextColor;
        }
        else
        {
            GameActions.instance._MadeMistake?.Invoke();
            isSolved = false;
            //_Text.color = Color.red;
            _Text.color = _FailedTextColor;
        }
    }
    public void SetPosition(int Row,int Collumn)
    {
        _Row = Row;
        _Collumn = Collumn;
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
    public void GiveHint()
    {
        _Text.gameObject.SetActive(true);
        _Text.text = _Number.ToString();
        isSolved = true;
    }
    public void EraseNumber()
    {
        isSolved = false;
        _Text.text = "";
        _Text.gameObject.SetActive(false);
    }
}
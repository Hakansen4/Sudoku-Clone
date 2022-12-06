using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] private GameObject _Square;
    [SerializeField] private float _GridDistance;
    [SerializeField] private int _RowAndColumnRange;
    private int _BoxesWidth;
    int _CreateRepetitions = 0;
    private Square[,] _AllSquares;
    private Vector2 _StartingPosition = new Vector2(-470, -280);
    private void Press(int x)
    {
        Debug.Log(x);
    }
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        _AllSquares = new Square[_RowAndColumnRange, _RowAndColumnRange];
        _BoxesWidth = (int)Mathf.Sqrt(_RowAndColumnRange);
    }
    private void Start()
    {
        GameActions.instance._PressedAction += Press;
        CreateGrid();
        FillIndependentBoxes();
    }
    private void OnDisable()
    {
        GameActions.instance._PressedAction -= Press;
    }

    private void Update()
    {
        FillSquares();
    }
    private void FillSquares()
    {
        for (int i = 0; i < _RowAndColumnRange; i+=_BoxesWidth)
        {
            for (int j = 0; j < _RowAndColumnRange; j+=_BoxesWidth)
            {
                if (i != j)
                    FillOtherBoxes(j, i);
            }
        }
        _CreateRepetitions++;
        if (_CreateRepetitions > 100    &&  !CheckGrid())
        {
            ResetGrid();
            _CreateRepetitions = 0;
        }
    }
    private bool CheckCreatFinished()
    {
        for (int i = 0; i < _RowAndColumnRange; i++)
        {
            for (int j = 0; j < _RowAndColumnRange; j++)
            {
                if (!_AllSquares[j, i].IsCreated())
                    return false;
            }
        }
        return true;
    }
    private bool CheckGrid()
    {
        for (int i = 0; i < _RowAndColumnRange; i++)
        {
            for (int j = 0; j < _RowAndColumnRange; j++)
            {
                if (!_AllSquares[j, i].IsCreated())
                    return false;
            }
        }
        return true;
    }
    private void CreateGrid()
    {
        int que = 1;
        GameObject _NewSquare;
        Vector2 _InstantiatePosition = _StartingPosition;
        for (int column = 0; column < _RowAndColumnRange; column++)
        {
            for (int line = 0; line < _RowAndColumnRange; line++)
            {
                _NewSquare = Instantiate(_Square, transform);
                _NewSquare.transform.localPosition = _InstantiatePosition;
                _NewSquare.GetComponent<Square>()._Rank = que++;
                _AllSquares[line,column] = _NewSquare.GetComponent<Square>();
                _InstantiatePosition += new Vector2(_GridDistance, 0);
            }
            _InstantiatePosition = new Vector2(_StartingPosition.x, _InstantiatePosition.y + _GridDistance);
        }
    }

    private void FillIndependentBoxes()
    {
        for (int i = 0; i < _RowAndColumnRange; i+=_BoxesWidth)
        {
            FillBox(i);
        }
    }

    private void FillBox(int rowandcol)
    {
        int _Number;
        for (int i = 0; i < _BoxesWidth; i++)
        {
            for (int j = 0; j < _BoxesWidth; j++)
            {
                _Number = (int)Random.RandomRange(1, _RowAndColumnRange + 1);
                if (!GridSetup.CheckBox(_Number, _AllSquares, rowandcol, rowandcol, _BoxesWidth))
                    j--;
                else
                    _AllSquares[rowandcol + j, rowandcol + i].SetNumber(_Number); 
            }
        }
    }
    private void FillOtherBoxes(int row,int collumn)
    {
        int _Number;
        for (int i = 0; i < _BoxesWidth; i++)
        {
            for (int j = 0; j < _BoxesWidth; j++)
            {
                _Number = (int)Random.RandomRange(1, _RowAndColumnRange + 1);
                if (GridSetup.CheckSquareValid(_Number, _AllSquares, row + j, collumn + i, _BoxesWidth))
                    _AllSquares[row + j, collumn + i].SetNumber(_Number);
            }
        }

    }
    private void ResetGrid()
    {
        for (int i = 0; i < _RowAndColumnRange; i++)
        {
            for (int j = 0; j < _RowAndColumnRange; j++)
            {
                _AllSquares[j, i].SetNumber(0);
                _AllSquares[j, i].ResetCreated();
            }
        }
        FillIndependentBoxes();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{

    [SerializeField] private GameObject _Square;
    [SerializeField] private float _GridDistance;
    [SerializeField] private int _RowAndColumnRange;
    private Square _PickedSquare;
    private int _BoxesWidth;
    int _CreateRepetitions = 0;
    bool _Created;
    private Square[,] _AllSquares;
    private Vector2 _StartingPosition = new Vector2(-455, -300);//-470,-280
    #region PaintBoxes
    private void Press(int _Row,int _Collumn)
    {
        _PickedSquare = _AllSquares[_Row, _Collumn];
        ClearPaints();
        PaintRowAndCollumn(_Row, _Collumn);
        PaintBoxe(_Row, _Collumn);
    }
    private void PaintRowAndCollumn(int _Row,int _Collumn)
    {
        for (int i = 0; i < _RowAndColumnRange; i++)
        {
            _AllSquares[i, _Collumn].HighlightSquare();
            _AllSquares[_Row, i].HighlightSquare();
        }
    }
    private void PaintBoxe(int Row,int Collumn)
    {
        int boxStarterRow = Row; int boxStarterColumn = Collumn;
        if (Row > 0)
            boxStarterRow = Row - Row % 3;
        if (Collumn > 0)
            boxStarterColumn = Collumn - Collumn % 3;

        for (int i = 0; i < _BoxesWidth; i++)
        {
            for (int j = 0; j < _BoxesWidth; j++)
            {
                _AllSquares[boxStarterRow + j, boxStarterColumn + i].HighlightSquare();
            }
        }

    }
    private void ClearPaints()
    {
        for (int i = 0; i < _RowAndColumnRange; i++)
        {
            for (int j = 0; j < _RowAndColumnRange; j++)
            {
                _AllSquares[j, i].ResetSquareColor();
            }
        }
    }
    #endregion
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
        GameActions.instance._Play += PlayTheGame;
        GameActions.instance._Hinted += GiveHint;
        GameActions.instance._Erase += EraseSquare;
        CreateGrid();
        FillIndependentBoxes();
    }
    private void OnDisable()
    {
        GameActions.instance._PressedAction -= Press;
        GameActions.instance._Play -= PlayTheGame;
        GameActions.instance._Hinted -= GiveHint;
        GameActions.instance._Erase -= EraseSquare;
    }
    private void PlayTheGame(int _PressedNumber)
    {
        _PickedSquare?.Play(_PressedNumber);
    }
    private void Update()
    {
        FillSquares();
        if (!_Created && CheckCreatFinished())
            StartTheGame();
        if (CheckGridCompleted())
            GameActions.instance._GameCompleted?.Invoke();
    }
    private void EraseSquare()
    {
        if (_PickedSquare != null)
            _PickedSquare.EraseNumber();
    }
    private void GiveHint()
    {
        if (_PickedSquare != null)
            _PickedSquare.GiveHint();
        else
            GameActions.instance._HintFailed?.Invoke();
    }
    private void StartTheGame()
    {
        for (int i = 0; i < PlayerPrefs.GetInt("Difficulty"); i++)
        {
            int _Number1 = (int)Random.RandomRange(0, 9);
            int _Number2 = (int)Random.RandomRange(0, 9);
            if (!_AllSquares[_Number1, _Number2].CheckSolved())
                _AllSquares[_Number1, _Number2].SetAsStarter();
            else
                i--;
        }
        _Created = true;
        GameActions.instance._LoadFinished?.Invoke();
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
        if (_CreateRepetitions > 1000    &&  !CheckGrid())
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
        GameObject _NewSquare;
        Vector2 _InstantiatePosition = _StartingPosition;
        for (int column = 0; column < _RowAndColumnRange; column++)
        {
            for (int line = 0; line < _RowAndColumnRange; line++)
            {
                _NewSquare = Instantiate(_Square, transform);
                _NewSquare.transform.localPosition = _InstantiatePosition;
                _NewSquare.GetComponent<Square>().SetPosition(line, column);
                _AllSquares[line,column] = _NewSquare.GetComponent<Square>();
                _InstantiatePosition += new Vector2(_GridDistance, 0);
            }
            _InstantiatePosition = new Vector2(_StartingPosition.x, _InstantiatePosition.y + _GridDistance);
        }
    }
    private bool CheckGridCompleted()
    {
        for (int i = 0; i < _RowAndColumnRange; i++)
        {
            for (int j = 0; j < _RowAndColumnRange; j++)
            {
                if (!_AllSquares[j, i].CheckSolved())
                    return false;
            }
        }
        return true;
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
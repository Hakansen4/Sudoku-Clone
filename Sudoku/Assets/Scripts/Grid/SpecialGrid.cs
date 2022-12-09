using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SpecialGrid : GridController
{
    private float _CurrentRotation;
    [SerializeField] private RectTransform[] _Sides;
    private void Start()
    {
        GameActions.instance._RotateGridDown += RotateDown;
        GameActions.instance._RotateGridUp += RotateUp;
    }
    private void OnDestroy()
    {
        GameActions.instance._RotateGridDown -= RotateDown;
        GameActions.instance._RotateGridUp -= RotateUp;
    }
    protected override void CreateGrid()
    {
        GameObject _NewSquare;
        int SideCounter = 0;
        Transform _ActiveSide = _Sides[SideCounter];
        _StartingPosition += new Vector2(0, _GridDistance * 3);
        Vector2 _InstantiatePosition = _StartingPosition;
        for (int column = 0; column < _RowAndColumnRange; column++)
        {
            if (column % 3 == 0 && column != 0)
            {
                _ActiveSide = _Sides[++SideCounter];
                _InstantiatePosition = _StartingPosition;
            }
            for (int line = 0; line < _RowAndColumnRange; line++)
            {
                _NewSquare = Instantiate(_Square, _ActiveSide);
                _NewSquare.transform.localPosition = _InstantiatePosition;
                _NewSquare.GetComponent<Square>().SetPosition(line, column);
                _AllSquares[line, column] = _NewSquare.GetComponent<Square>();
                _InstantiatePosition += new Vector2(_GridDistance, 0);
            }
            _InstantiatePosition = new Vector2(_StartingPosition.x, _InstantiatePosition.y + _GridDistance);
        }
        _Sides[2].pivot = new Vector2(0.5f, 0.645f);
    }
    private void RotateUp()
    {
        switch (_CurrentRotation)
        {
            case 0:
                _Sides[1].pivot = new Vector2(0.5f, 0.645f);
                _Sides[1].DORotate(new Vector3(90, 0, 0), 1);
                _Sides[2].DORotate(Vector3.zero, 1);
                _CurrentRotation = 90;
                break;
            case -90:
                _Sides[1].pivot = new Vector2(0.5f, 0.5f);
                _Sides[0].DORotate(new Vector3(-90, 0, 0), 1);
                _Sides[1].DORotate(Vector3.zero, 1);
                _CurrentRotation = 0;
                break;
            case 90:
                break;
            default:
                break;
        }
    }
    private void RotateDown()
    {
        switch (_CurrentRotation)
        {
            case 0:
                _Sides[1].pivot = new Vector2(0.5f, 0.5f);
                _Sides[0].DORotate(Vector3.zero, 1);
                _Sides[1].DORotate(new Vector3(-90, 0, 0), 1);
                _CurrentRotation = -90;
                break;
            case -90:
                break;
            case 90:
                _Sides[1].pivot = new Vector2(0.5f, 0.645f);
                _Sides[1].DORotate(Vector3.zero, 1);
                _Sides[2].DORotate(new Vector3(90, 0, 0), 1);
                _CurrentRotation = 0;
                break;
            default:
                break;
        }
        //if(_CurrentRotation == 0)
        //{
            
            
        //}
        //else if(_CurrentRotation == -90)
        //{

        //}
    }
}
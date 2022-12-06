using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSetup : MonoBehaviour
{
    public static bool CheckBox(int Number,Square[,] _Grid,int row,int column,int BoxWidth)
    {
        for (int i = 0; i < BoxWidth; i++)
        {
            for (int j = 0; j < BoxWidth; j++)
            {
                if (_Grid[row + j, column + i].GetNumber() == Number)
                    return false;
            }
        }
        return true;
    }
    public static bool CheckRow(int Number, Square[,] _Grid,int height)
    {
        for (int i = 0; i < Mathf.Sqrt(_Grid.Length); i++)
        {
            if (_Grid[i, height].GetNumber() == Number)
                return false;
        }
        return true;
    }
    public static bool CheckColumn(int Number, Square[,] _Grid, int width)
    {
        for (int i = 0; i < Mathf.Sqrt(_Grid.Length); i++)
        {
            if (_Grid[width, i].GetNumber() == Number)
                return false;
        }
        return true;
    }
    public static bool CheckSquareValid(int number,Square[,] _Grid, int row, int column,int BoxesWidth)
    {
        //Finidng their boxes for control it.
        int boxStarterRow = row; int boxStarterColumn = column;
        if (row > 0)
            boxStarterRow = row - row % 3;
        if (column > 0)
            boxStarterColumn = column - column % 3;

        if (CheckRow(number, _Grid, column)
            && CheckColumn(number, _Grid, row)
                && CheckBox(number, _Grid, boxStarterRow, boxStarterColumn, BoxesWidth))
            return true;
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateGridButtonsController : MonoBehaviour
{
    [SerializeField] private Button _RotateUp;
    [SerializeField] private Button _RotateDown;
    private void OnEnable()
    {
        _RotateUp.onClick.AddListener(RotateGridUp);
        _RotateDown.onClick.AddListener(RotateGridDown);
    }
    private void OnDisable()
    {
        _RotateUp.onClick.RemoveListener(RotateGridUp);
        _RotateDown.onClick.RemoveListener(RotateGridDown);
    }
    private void RotateGridUp()
    {
        GameActions.instance._RotateGridUp?.Invoke();
    }
    private void RotateGridDown()
    {
        GameActions.instance._RotateGridDown?.Invoke();
    }
}

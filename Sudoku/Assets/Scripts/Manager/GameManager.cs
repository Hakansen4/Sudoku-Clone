using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int MaxHint;
    [SerializeField] private int MaxMistake;
    [SerializeField] private TextMeshProUGUI _MistakesText;
    [SerializeField] private TextMeshProUGUI _TimerText;
    [SerializeField] private GameObject _LevelFailedScreen;
    [SerializeField] private GameObject _LevelCompletedScreen;
    private bool _StartTimer;
    private float _Timer;
    private float _StartingTime;
    private int _MistakeCount;
    private int _HintCount;

    private void Awake()
    {
        Init();
    }
    private void Start()
    {
        GameActions.instance._MadeMistake += MadeMistake;
        GameActions.instance._GameCompleted += CompleteGame;
        GameActions.instance._LoadFinished += StartTimer;
        GameActions.instance._Hinted += CheckHint;
        GameActions.instance._HintFailed += FailedHint;
    }
    private void OnDisable()
    {
        GameActions.instance._MadeMistake -= MadeMistake;
        GameActions.instance._GameCompleted -= CompleteGame;
        GameActions.instance._LoadFinished -= StartTimer;
        GameActions.instance._Hinted -= CheckHint;
        GameActions.instance._HintFailed -= FailedHint;
    }
    private void Init()
    {
        _MistakeCount = 0;
        _HintCount = 0;
        _MistakesText.text = _MistakeCount.ToString() + " / " + MaxMistake.ToString();
    }
    private void Update()
    {
        if(_StartTimer)
        {
            _Timer = Time.time - _StartingTime;
            _TimerText.text = (Math.Round(_Timer,2)).ToString();
        }
    }
    private void FailedHint()
    {
        _HintCount--;
    }
    private void CheckHint()
    {
        _HintCount++;
        if (_HintCount == MaxHint)
            GameActions.instance._HintOver?.Invoke();
    }
    private void StartTimer()
    {
        _StartingTime = Time.time;
        _StartTimer = true;
    }
    private void MadeMistake()
    {
        _MistakeCount++;
        _MistakesText.text = _MistakeCount.ToString() + " / " + MaxMistake.ToString();
        if (_MistakeCount == MaxMistake)
            GameOver();
    }
    private void GameOver()
    {
        _LevelFailedScreen.SetActive(true);
    }
    private void CompleteGame()
    {
        _LevelCompletedScreen.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameActions : MonoBehaviour
{
    public static GameActions instance { get; private set; }

    public UnityAction<int,int> _PressedAction;
    public UnityAction<int> _Play;
    public UnityAction _GameCompleted;
    public UnityAction _LoadFinished;
    public UnityAction _MadeMistake;
    public UnityAction _Hinted;
    public UnityAction _HintFailed;
    public UnityAction _HintOver;
    public UnityAction _Erase;
    
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenController : MonoBehaviour
{
    [SerializeField] private Animator _Anim;
    private void Start()
    {
        if (GameActions.instance != null)
            GameActions.instance._LoadFinished += CloseLoadingScreen;
    }
    private void OnDisable()
    {
        if (GameActions.instance != null)
            GameActions.instance._LoadFinished -= CloseLoadingScreen;
    }
    private void CloseLoadingScreen()
    {
        _Anim.SetTrigger("Close");
    }
}

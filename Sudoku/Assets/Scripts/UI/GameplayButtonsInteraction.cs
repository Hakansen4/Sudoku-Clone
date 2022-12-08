using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayButtonsInteraction : MonoBehaviour
{
    //[SerializeField] private Button[] PlayButtons;
    [Header("Play Buttons")]
    [SerializeField] private Button _PlayButton1;
    [SerializeField] private Button _PlayButton2;
    [SerializeField] private Button _PlayButton3;
    [SerializeField] private Button _PlayButton4;
    [SerializeField] private Button _PlayButton5;
    [SerializeField] private Button _PlayButton6;
    [SerializeField] private Button _PlayButton7;
    [SerializeField] private Button _PlayButton8;
    [SerializeField] private Button _PlayButton9;
    [SerializeField] private Button _Hint;
    [SerializeField] private Button _Erase;
    [Header("Level Pass Buttons")]
    [SerializeField] private Button _FailedRetry;
    [SerializeField] private Button _FailedMenu;
    [SerializeField] private Button _CompletedMenu;
    private void Start()
    {
        GameActions.instance._HintOver += DeactiveHint;
    }
    private void OnEnable()
    {
        //for (int i = 0; i < PlayButtons.Length; i++)
        //{
        //    PlayButtons[i].onClick.AddListener(() => { PlayButtonClick(i); });
        //}
        _PlayButton1.onClick.AddListener(PlayButtonClick_1);
        _PlayButton2.onClick.AddListener(PlayButtonClick_2);
        _PlayButton3.onClick.AddListener(PlayButtonClick_3);
        _PlayButton4.onClick.AddListener(PlayButtonClick_4);
        _PlayButton5.onClick.AddListener(PlayButtonClick_5);
        _PlayButton6.onClick.AddListener(PlayButtonClick_6);
        _PlayButton7.onClick.AddListener(PlayButtonClick_7);
        _PlayButton8.onClick.AddListener(PlayButtonClick_8);
        _PlayButton9.onClick.AddListener(PlayButtonClick_9);
        _FailedRetry.onClick.AddListener(RetryGame);
        _FailedMenu.onClick.AddListener(ReturnMenu);
        _CompletedMenu.onClick.AddListener(ReturnMenu);
        _Hint.onClick.AddListener(GetHint);
        _Erase.onClick.AddListener(Erase);
    }
    private void OnDisable()
    {
        //for (int i = 0; i < PlayButtons.Length; i++)
        //{
        //    PlayButtons[i].onClick.RemoveListener(() => { PlayButtonClick(i); });
        //}
        GameActions.instance._HintOver -= DeactiveHint;
        _PlayButton1.onClick.RemoveListener(PlayButtonClick_1);
        _PlayButton2.onClick.RemoveListener(PlayButtonClick_2);
        _PlayButton3.onClick.RemoveListener(PlayButtonClick_3);
        _PlayButton4.onClick.RemoveListener(PlayButtonClick_4);
        _PlayButton5.onClick.RemoveListener(PlayButtonClick_5);
        _PlayButton6.onClick.RemoveListener(PlayButtonClick_6);
        _PlayButton7.onClick.RemoveListener(PlayButtonClick_7);
        _PlayButton8.onClick.RemoveListener(PlayButtonClick_8);
        _PlayButton9.onClick.RemoveListener(PlayButtonClick_9);
        _FailedRetry.onClick.RemoveListener(RetryGame);
        _FailedMenu.onClick.RemoveListener(ReturnMenu);
        _CompletedMenu.onClick.RemoveListener(ReturnMenu);
        _Hint.onClick.RemoveListener(GetHint);
        _Erase.onClick.RemoveListener(Erase);
    }
    private void Erase()
    {
        GameActions.instance._Erase?.Invoke();
    }
    private void DeactiveHint()
    {
        _Hint.interactable = false;
    }
    private void PlayButtonClick_1()
    {
        GameActions.instance._Play?.Invoke(1);
    }
    private void PlayButtonClick_2()
    {
        GameActions.instance._Play?.Invoke(2);
    }
    private void PlayButtonClick_3()
    {
        GameActions.instance._Play?.Invoke(3);
    }
    private void PlayButtonClick_4()
    {
        GameActions.instance._Play?.Invoke(4);
    }
    private void PlayButtonClick_5()
    {
        GameActions.instance._Play?.Invoke(5);
    }
    private void PlayButtonClick_6()
    {
        GameActions.instance._Play?.Invoke(6);
    }
    private void PlayButtonClick_7()
    {
        GameActions.instance._Play?.Invoke(7);
    }
    private void PlayButtonClick_8()
    {
        GameActions.instance._Play?.Invoke(8);
    }
    private void PlayButtonClick_9()
    {
        GameActions.instance._Play?.Invoke(9);
    }
    private void ReturnMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    private void RetryGame()
    {
        SceneManager.LoadScene("Gameplay");
    }
    private void GetHint()
    {
        GameActions.instance._Hinted?.Invoke();
    }
}
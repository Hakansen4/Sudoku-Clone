using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Animator _LoadingAnim;
    [SerializeField] private Animator _DifficultyPanel;
    [SerializeField] private Button _ClassicSudoku;
    [SerializeField] private Button _RotaterSudoku;
    [SerializeField] private Button _SuperEasy;
    [SerializeField] private Button _Easy;
    [SerializeField] private Button _Medium;
    [SerializeField] private Button _Hard;
    [SerializeField] private Button _Back;
    private GameType _Type;
    private void OnEnable()
    {
        _ClassicSudoku.onClick.AddListener(PlayClassicSudoku);
        _RotaterSudoku.onClick.AddListener(PlayRotaterSudoku);
        _SuperEasy.onClick.AddListener(SuperEasyGame);
        _Easy.onClick.AddListener(EasyGame);
        _Medium.onClick.AddListener(MediumGame);
        _Hard.onClick.AddListener(HardGame);
        _Back.onClick.AddListener(GoBack);
    }
    private void OnDisable()
    {
        _ClassicSudoku.onClick.RemoveListener(PlayClassicSudoku);
        _RotaterSudoku.onClick.RemoveListener(PlayRotaterSudoku);
        _SuperEasy.onClick.RemoveListener(SuperEasyGame);
        _Easy.onClick.RemoveListener(EasyGame);
        _Medium.onClick.RemoveListener(MediumGame);
        _Hard.onClick.RemoveListener(HardGame);
        _Back.onClick.RemoveListener(GoBack);
    }
    private void PlayRotaterSudoku()
    {
        _Type = GameType.Rotater;
        _DifficultyPanel.SetTrigger("Open");
    }
    private void PlayClassicSudoku()
    {
        _Type = GameType.Classic;
        _DifficultyPanel.SetTrigger("Open");
    }
    private IEnumerator LoadGameplay()
    {
        _LoadingAnim.gameObject.SetActive(true);
        _LoadingAnim.SetTrigger("Open");
        yield return new WaitForSeconds(3);
        switch (_Type)
        {
            case GameType.Classic:
                SceneManager.LoadScene("ClassicGameplay");
                break;
            case GameType.Rotater:
                SceneManager.LoadScene("RotaterGameplay");
                break;
            default:
                break;
        }
    }
    private void SuperEasyGame()
    {
        PlayerPrefs.SetInt("Difficulty", 75);
        StartCoroutine(LoadGameplay());
    }
    private void EasyGame()
    {
        PlayerPrefs.SetInt("Difficulty", 70);
        StartCoroutine(LoadGameplay());
    }
    private void MediumGame()
    {
        PlayerPrefs.SetInt("Difficulty", 60);
        StartCoroutine(LoadGameplay());
    }
    private void HardGame()
    {
        PlayerPrefs.SetInt("Difficulty", 50);
        StartCoroutine(LoadGameplay());
    }
    private void GoBack()
    {
        _DifficultyPanel.SetTrigger("Close");
    }
}

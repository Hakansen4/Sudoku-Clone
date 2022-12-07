using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Animator _LoadingAnim;
    [SerializeField] private Animator _DifficultyPanel;
    [SerializeField] private Button _NewGame;
    [SerializeField] private Button _SuperEasy;
    [SerializeField] private Button _Easy;
    [SerializeField] private Button _Medium;
    [SerializeField] private Button _Hard;
    [SerializeField] private Button _Back;
    private void OnEnable()
    {
        _NewGame.onClick.AddListener(NewGame);
        _SuperEasy.onClick.AddListener(SuperEasyGame);
        _Easy.onClick.AddListener(EasyGame);
        _Medium.onClick.AddListener(MediumGame);
        _Hard.onClick.AddListener(HardGame);
        _Back.onClick.AddListener(GoBack);
    }
    private void OnDisable()
    {
        _NewGame.onClick.RemoveListener(NewGame);
        _SuperEasy.onClick.RemoveListener(SuperEasyGame);
        _Easy.onClick.RemoveListener(EasyGame);
        _Medium.onClick.RemoveListener(MediumGame);
        _Hard.onClick.RemoveListener(HardGame);
        _Back.onClick.RemoveListener(GoBack);
    }

    private void NewGame()
    {
        _DifficultyPanel.SetTrigger("Open");
    }
    private IEnumerator LoadGameplay()
    {
        _LoadingAnim.gameObject.SetActive(true);
        _LoadingAnim.SetTrigger("Open");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Gameplay");
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

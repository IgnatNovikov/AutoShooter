using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private Text _windowText;

    [Header("Animations")]
    [SerializeField] private string show_animation;
    [SerializeField] private string hide_animation;

    private GameManager _gameManager;
    private Animator _animator;

    public void Victory()
    {
        ShowWindow("VICTORY");
    }

    public void Defeat()
    {
        ShowWindow("DEFEAT");
    }

    public void OnRestart()
    {
        GameManager.Instance.RestartGame();
        HideWindow();
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _gameManager = GameManager.Instance;
    }

    private void ShowWindow(string text)
    {
        Time.timeScale = 0f;

        _windowText.text = text;
        _animator.Play(show_animation);
    }

    private void HideWindow()
    {
        _animator.Play(hide_animation);

        Time.timeScale = 1f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Text window_text;

    [SerializeField] string show_animation;
    [SerializeField] string hide_animation;

    [SerializeField] SpawnerController spawner;
    [SerializeField] PlayerController player;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void ShowWindow(string text)
    {
        Time.timeScale = 0f;

        window_text.text = text;
        animator.Play(show_animation);
    }

    void HideWindow()
    {
        animator.Play(hide_animation);

        Time.timeScale = 1f;
    }

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
        HideWindow();

        spawner.Restart();
        player.Restart();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DialogAsync
{
    private Text _text;
    private bool _isClosed;

    public DialogAsync(Text text)
    {
        _text = text;
    }

    public async void StartDialog(GameObject dialogPanel, List<string> phrases)
    {
        dialogPanel.SetActive(true);

        foreach (string phrase in phrases)
        {
            await ShowMessage(phrase);
        }

        dialogPanel.SetActive(false);
    }

    private async Task ShowMessage(string text)
    {
        Debug.Log("Dialog show");

        _isClosed = false;
        _text.text = text;

        
        while (!_isClosed)
        {
            await Task.Yield();
        }

        Debug.Log("dialog closed");
    }

    public void Close()
    {
        _isClosed = true;
    }
}

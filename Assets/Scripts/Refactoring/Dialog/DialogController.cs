using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private GameObject _dialogPanel;
    [SerializeField] private List<string> _phrases = new List<string>();

    public System.Action<GameObject> onClick;
    private DialogAsync _dialog;

    void Start()
    {
        _dialog = new DialogAsync(_text);
        _dialog.StartDialog(_dialogPanel, _phrases);
    }

    public void OnCloseButton()
    {
        _dialog.Close();
    }
}

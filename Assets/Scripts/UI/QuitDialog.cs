using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuitDialog : Dialog
{
    [SerializeField] private TextMeshProUGUI no, yes, text, title;
    [SerializeField] private Button bgListener, closeButton;

    private void Start()
    {
        bgListener.onClick.AddListener(Close);
        closeButton.onClick.AddListener(Close);
    }

    public override void Build()
    {
        base.Build();

        no.text = "No";
        yes.text = "Yes";
        text.text = "Are you sure you want to quit the game?";
        title.text = "Quit";
    }
}

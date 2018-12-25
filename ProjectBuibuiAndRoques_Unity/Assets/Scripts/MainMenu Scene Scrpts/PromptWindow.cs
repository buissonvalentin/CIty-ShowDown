using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptWindow : MonoBehaviour
{
    public Button confirmButton;
    public Button cancelButton;
    public Text titleText;
    public Text contentText;

    public delegate void PromptWindowAction();

    PromptWindowAction confirm;
    PromptWindowAction cancel;

    // Start is called before the first frame update
    void Start()
    {
        confirmButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            confirm();
            ClearAction();
        });

        cancelButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            cancel();
            ClearAction();
        });
    }

    public void OpenPrompt(PromptWindowAction confirmAction, PromptWindowAction cancelAction, string title, string content)
    {
        gameObject.SetActive(true);
        titleText.text = title;
        contentText.text = content;
        confirm += confirmAction;
        cancel += cancelAction;
    }

    void ClearAction()
    {
        foreach (Delegate d in confirm.GetInvocationList())
        {
            confirm -= (PromptWindowAction)d;
        }
        foreach (Delegate d in cancel.GetInvocationList())
        {
            cancel -= (PromptWindowAction)d;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool ConfirmPrompt()
    {
        return true;
    }
}

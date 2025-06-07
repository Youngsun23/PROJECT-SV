using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : SingletonBase<DialogueManager>
{
    [SerializeField] private TextMeshProUGUI lineText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image portrait;

    private DialogueData currentDialogue;
    private int currentTextLine;

    [Range(0f, 1f)]
    [SerializeField] private float visibleTextPercentage;
    [SerializeField] private float timePerLetter = 0.05f;
    private float totalTimeToType, currentTime;
    private string lineToShow;

    private void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            NextLine();
        }
        TypeLine();
    }

    public void StartDialogue(DialogueData dialogue)
    {
        currentDialogue = dialogue;
        currentTextLine = 0;
        ShowSpeakerInfo();
        NewLine();
        Show();
    }

    private void ShowSpeakerInfo()
    {
        nameText.text = currentDialogue.SpeakerName;
        portrait.sprite = currentDialogue.SpeakerPortrait;
    }

    private void NextLine()
    {
        if(visibleTextPercentage < 1f)
        {
            visibleTextPercentage = 1f;
            ShowText();
            return;
        }

        if(currentTextLine >= currentDialogue.Script.Count)
        {
            EndDialogue();
        }
        else
        {
            NewLine();
            // lineText.text = currentDialogue.Script[currentTextLine];
        }
    }

    private void NewLine()
    {
        lineToShow = currentDialogue.Script[currentTextLine];
        totalTimeToType = lineToShow.Length * timePerLetter;
        currentTime = 0f;
        visibleTextPercentage = 0f;
        lineText.text = "";

        currentTextLine += 1;
    }

    private void EndDialogue()
    {
        Hide();
        Debug.Log($"Dialogue Ended");
    }

    private void TypeLine()
    {
        if (visibleTextPercentage > 1f) return;

        currentTime += Time.deltaTime;
        visibleTextPercentage = currentTime / totalTimeToType;
        visibleTextPercentage = Mathf.Clamp(visibleTextPercentage, 0f, 1f);
        ShowText();
    }

    private void ShowText()
    {
        int letterCount = (int)(lineToShow.Length * visibleTextPercentage);
        lineText.text = lineToShow.Substring(0, letterCount);
    }

    // UIManager 도입 전 Temp
    private void Show()
    {
        this.gameObject.SetActive(true);
    }
    private void Hide()
    {
        this.gameObject.SetActive(false);
    }
}

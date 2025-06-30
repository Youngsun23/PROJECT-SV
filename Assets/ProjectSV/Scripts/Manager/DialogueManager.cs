using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
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

    private bool isTyping = true;

    private void Update()
    {
        if (currentDialogue == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            isTyping = true;
            NextLine();
        }
        if(isTyping)
        {
            TypeLine();
        }
    }

    public void StartDialogue(DialogueData dialogue)
    {
        currentDialogue = dialogue;
        currentTextLine = 0;
        ShowSpeakerInfo();
        NewLine();
        // Show();
    }

    private void ShowSpeakerInfo()
    {
        nameText.text = currentDialogue.SpeakerName;
        portrait.sprite = currentDialogue.SpeakerPortrait;
    }

    private void NextLine()
    {
        if (visibleTextPercentage < 1f)
        {
            visibleTextPercentage = 1f;
            // TypeLineAtOnce(); // 안 먹는듯?
            isTyping = false;
            ShowText();
            return;
        }

        if (currentTextLine >= currentDialogue.Script.Count)
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
        // Debug.Log($"{currentTextLine}번째 대사 => {lineToShow}");
        totalTimeToType = lineToShow.Length * timePerLetter;
        currentTime = 0f;
        visibleTextPercentage = 0f;
        lineText.text = "";

        currentTextLine += 1;
    }

    private void EndDialogue()
    {
        currentDialogue = null;
        // Hide();

        UIManager.Hide<DialogueUI>(UIType.Dialogue);
    }

    private void TypeLine()
    {
        if (visibleTextPercentage > 1f) return;

        currentTime += Time.deltaTime;
        visibleTextPercentage = currentTime / totalTimeToType;
        visibleTextPercentage = Mathf.Clamp(visibleTextPercentage, 0f, 1f);
        ShowText();
    }

    private void TypeLineAtOnce()
    {
        lineText.text = lineToShow;
    }

    private void ShowText()
    {
        int letterCount = (int)(lineToShow.Length * visibleTextPercentage);
        lineText.text = lineToShow.Substring(0, letterCount);
    }

    //// UIManager 도입 전 Temp
    //private void Show()
    //{
    //    this.gameObject.SetActive(true);
    //}
    //private void Hide()
    //{
    //    this.gameObject.SetActive(false);
    //}
}

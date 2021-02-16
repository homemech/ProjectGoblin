using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueDisplayer : MonoBehaviour
{
    [SerializeField] private StringValue stringText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialogueParentObject;
    private bool dialogueIsActive = false;

    private void Start()
    {
        dialogueParentObject.SetActive(dialogueIsActive);
    }

    public void ActivateDialogueWindow()
    {
        dialogueIsActive = !dialogueIsActive;
        if (dialogueIsActive)
        {
            SetDialogue();
        }
        else
        {
            DeactivateDialogue();
        }
    }

    void SetDialogue()
    {
        dialogueParentObject.SetActive(true);
        dialogueText.text = stringText.value;
    }

    void DeactivateDialogue()
    {
        dialogueParentObject.SetActive(false);
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshPro dialogueText;         // Texte du dialogue affiché
    public TextMeshPro speakerNameText;      // Nom du locuteur
    public GameObject dialogueUI;     // Panneau de dialogue

    private string[] dialogueLines;   // Lignes du dialogue
    private string[] speakers;        // Locuteurs associés aux lignes
    private int currentLineIndex;     // Index de la réplique actuelle
    private bool isDialogueActive = false;

    public void StartDialogue(string[] lines, string[] speakersNames)
    {
        dialogueLines = lines;
        speakers = speakersNames;
        currentLineIndex = 0;
        isDialogueActive = true;
        dialogueUI.SetActive(true);
        ShowNextLine();
    }

    public void ShowNextLine()
    {
        if (currentLineIndex < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLineIndex];
            speakerNameText.text = speakers[currentLineIndex];
            currentLineIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
        dialogueUI.SetActive(false);
    }

    void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space)) // Appuie pour avancer
        {
            ShowNextLine();
        }
    }
}

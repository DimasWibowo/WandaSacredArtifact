using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public Image characterIcon;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;

    private Queue<DialogueLine> lines;
    
    public bool isDialogueActive = false;
    public bool isTyping = false;

    public float typingSpeed = 0.05f;

    public Animator animator;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        lines = new Queue<DialogueLine>();
    }

private void Update()
{
    if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
    {
        if (isTyping)
        {
            // Jika sedang mengetik, jangan lakukan apa pun saat tombol spasi ditekan
            return;
        }
        else if (lines.Count > 0)
        {
            // Jika tidak sedang mengetik dan masih ada baris di dalam antrian
            DisplayNextDialogueLine();
        }
        else
        {
            // Jika tidak sedang mengetik dan tidak ada lagi baris di dalam antrian
            EndDialogue();
        }
    }
}




    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;
        animator.SetBool("isDialogueActive", true);

        animator.Play("show");

        lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();
    }

	public void DisplayNextDialogueLine()
	{
		if (isTyping) // Jika sedang mengetik, hentikan fungsi ini dan tunggu sampai pengetikan selesai
		{
			return;
		}

		if (lines.Count == 0)
		{
			EndDialogue();
			return;
		}

		DialogueLine currentLine = lines.Dequeue();

		characterIcon.sprite = currentLine.character.icon;
		characterName.text = currentLine.character.name;

		StopAllCoroutines();
		StartCoroutine(TypeSentence(currentLine.line));
	}

    IEnumerator TypeSentence(string sentence)
    {
        dialogueArea.text = "";
        isTyping = true;

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        animator.SetBool("isDialogueActive", false);
        animator.Play("hide");
    }
}
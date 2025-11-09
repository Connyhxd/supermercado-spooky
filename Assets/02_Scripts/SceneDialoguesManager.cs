using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneDialoguesManager : MonoBehaviour
{
    public DialogueScriptable dialogue;
    public TextMeshProUGUI dialogueText, nameText;
    public Image spriteCharacter;

    public bool isTalking;
    public int phraseIndex;

    public string currentScene;

    public bool isTypeWriterEnded;

    private void Start()
    {
        Talk();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            Talk();
        }
    }

    public void Talk()
    {
        if (isTalking)
        {
            if (phraseIndex < dialogue.dialogue.Length - 1)
            {
                if (isTypeWriterEnded)
                {
                    phraseIndex++;
                    Refresh();
                }
                else
                {
                    StopCoroutine("TypeWriter");
                    dialogueText.text = dialogue.dialogue[phraseIndex].phrase;
                    isTypeWriterEnded = true;
                }
            }
            else
            {
                if (isTypeWriterEnded)
                {
                    EndDialogue();
                }
                else
                {
                    StopCoroutine("TypeWriter");
                    dialogueText.text = dialogue.dialogue[phraseIndex].phrase;
                    isTypeWriterEnded = true;
                }
            }
        }
        else
        {
            if (dialogue != null)
            {
                Refresh();
                isTalking = true;
            }
        }
    }

    public void Refresh()
    {
        spriteCharacter.sprite = dialogue.dialogue[phraseIndex].characterSprite;
        nameText.text = dialogue.dialogue[phraseIndex].characterName;
        StartCoroutine("TypeWriter");
    }

    private IEnumerator TypeWriter()
    {
        isTypeWriterEnded = false;
        dialogueText.text = string.Empty;
        foreach (char text in dialogue.dialogue[phraseIndex].phrase)
        {
            dialogueText.text += text;
            yield return new WaitForSeconds(0.1f);
        }
        isTypeWriterEnded = true;
    }

    public void EndDialogue()
    {
        StopAllCoroutines();

        isTalking = false;
        isTypeWriterEnded = true;

        if (currentScene == "Intro")
        {
            Cursor.lockState = CursorLockMode.Locked;
            SceneManager.LoadScene("Test");
        }
        else if (currentScene == "BadEnd")
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("MainMenu");
        }
        else if (currentScene == "GoodEnd")
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("MainMenu");
        }
    }
}

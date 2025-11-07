using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public AudioManager audioji;

    [Header("CONVERSATION")]
    public DialogueScriptable dialoguelol;
    public TextMeshProUGUI dialogueText, nameText;
    public Image spriteCharacter;
    public GameObject UIDIALOGUE;

    [Header("SYSRTEM")]
    public bool isTalking;
    public int phaseIndex;
    public bool isTypeEnded;
    public bool canTalk;

    private void Start()
    {
        canTalk = false;
        UIDIALOGUE.SetActive(false);
    }

    private void Update()
    {
        if (canTalk == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Talk();
            }
        }

        UIDIALOGUE.SetActive(isTalking);
    }

    public void Talk()
    {
        if(isTalking)
        {
            if (phaseIndex < dialoguelol.dialogue.Length - 1)
            {
                if(isTypeEnded)
                {
                    phaseIndex++;
                    Refresh();
                }
                else
                {
                    StopCoroutine("TypeWriter");
                    dialogueText.text = dialoguelol.dialogue[phaseIndex].phrase;
                    isTypeEnded = true;
                }
            }
            else
            {
                if (isTypeEnded)
                {
                    EndDialogue();
                    audioji.sfxSound.resource = audioji.dialogueEndSound;
                    audioji.sfxSound.Play();
                }
                else
                {
                    StopCoroutine("TypeWriter");
                    dialogueText.text = dialoguelol.dialogue[phaseIndex].phrase;
                    isTypeEnded= true;
                }
            }
        }
        else
        {
            if (dialoguelol != null)
            {
                Refresh();
                isTalking = true;
                audioji.sfxSound.resource = audioji.dialogueStartSound;
                audioji.sfxSound.Play();
            }
        }
    }

    public void Refresh()
    {
        spriteCharacter.sprite = dialoguelol.dialogue[phaseIndex].characterSprite;
        nameText.text = dialoguelol.dialogue[phaseIndex].characterName;
        StartCoroutine("TypeWriter");
    }

    private IEnumerator TypeWriter()
    {
        isTypeEnded = false;
        dialogueText.text = string.Empty;
        foreach (char text in dialoguelol.dialogue[phaseIndex].phrase)
        {
            dialogueText.text += text;
            yield return new WaitForSeconds(0.1f);
        }
        isTypeEnded = true;
    }    

    public void StartConversation(DialogueScriptable newDialogue)
    {
        if (isTalking)
            return;

        dialoguelol = newDialogue;
        phaseIndex = 0;
        isTalking = false;
    }

    public void CanTalk(bool state)
    {
        canTalk = state;

        if (!state && isTalking)
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        nameText.text = string.Empty;
        dialogueText.text = string.Empty;
        spriteCharacter.sprite = null;
        phaseIndex = 0;
        isTalking = false;
        isTypeEnded = true;
    }
}

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("CONVERSATION")]
    public DialogueScriptable dialoguelol;
    public TextMeshProUGUI dialogueText, nameText;
    public Image spriteCharacter;

    [Header("SYSRTEM")]
    public bool isTalking;
    public int phaseIndex;
    public bool isTypeEnded;
    public bool UIDialogue;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            Talk();
        }
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
                    nameText.text = string.Empty;
                    dialogueText.text += string.Empty;
                    spriteCharacter.sprite = null;
                    phaseIndex = 0;
                    isTalking = false;
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
            Refresh();
            isTalking = true;
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
}

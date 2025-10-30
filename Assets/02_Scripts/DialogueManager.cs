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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            Talk();
        }
    }

    public void Talk()
    {

    }
}

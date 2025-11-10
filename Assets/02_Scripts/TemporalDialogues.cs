using System.Collections;
using TMPro;
using UnityEngine;

public class TemporalDialogues : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;

    public int currentEvent;
    public GameObject thisEvent;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (currentEvent == 0)
            {
                StartCoroutine(ShowMessage("There's an empty cart over there, I should use it to put the items I'll buy."));
            }
            if (currentEvent == 1)
            {
                StartCoroutine(ShowMessage("... I shouldn't be here."));
            }
        }
    }


    private IEnumerator ShowMessage(string message)
    {
        dialogueText.text = message;
        dialogueText.gameObject.SetActive(true);

        yield return new WaitForSeconds(4f);

        dialogueText.gameObject.SetActive(false);
        thisEvent.SetActive(false);
    }
}

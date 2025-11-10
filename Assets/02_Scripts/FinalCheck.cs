using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalCheck : MonoBehaviour
{
    public Inventory cartInventory;
    public GameObject checkResultUIPrompt;

    private bool canCheckResult = false;

    void Start()
    {
        if (cartInventory == null)
        {
            cartInventory = FindAnyObjectByType<Inventory>();
        }
        if (checkResultUIPrompt != null)
        {
            checkResultUIPrompt.SetActive(false);
        }
    }

    private void Update()
    {
        bool showCheckResultUI = canCheckResult && cartInventory != null && cartInventory.purchaseMade;
        if (checkResultUIPrompt != null)
        {
            checkResultUIPrompt.SetActive(showCheckResultUI);
        }

        if (canCheckResult && cartInventory != null && cartInventory.purchaseMade && Input.GetKeyDown(KeyCode.E))
        {
            CheckShoppingResult();
            if (checkResultUIPrompt != null) checkResultUIPrompt.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canCheckResult = true;
            Debug.Log("Jugador en zona de verificacion");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canCheckResult = false;
        }
    }

    public void CheckShoppingResult()
    {
        if (cartInventory == null)
        {
            Debug.LogError("ERROR");
            return;
        }

        if (cartInventory.boletaText != null && cartInventory.boletaText.gameObject.activeSelf)
        {
            if (cartInventory.lastPurchaseCorrect)
            {
                if (cartInventory.extraItemsBought)
                {
                    Debug.Log("Compra Correcta but Objetos Extra");
                    UnityEngine.SceneManagement.SceneManager.LoadScene("NeutralEnd");
                }
                else
                {
                    Debug.Log("Compra Correcta");
                    UnityEngine.SceneManagement.SceneManager.LoadScene("GoodEnd");
                }
            }

            else
            {
                Debug.Log("Compra Incorrecta");
                SceneManager.LoadScene("BadEnd");
            }
        }
        else
        {
            Debug.Log("Primero pasa por caja para obtener la boleta.");
        }
    }
}

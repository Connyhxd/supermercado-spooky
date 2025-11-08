using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public AudioManager audioji;
    public List<ItemTemplate> itemsInCart = new List<ItemTemplate>();

    public ListGenerator listGenerator;

    public TextMeshProUGUI boletaText;

    [HideInInspector] public bool lastPurchaseCorrect = false;
    [HideInInspector] public bool purchaseMade = false;

    public bool canCheckout = false;
    public GameObject checkoutUIPrompt;

    private void Update()
    {
        bool showCheckoutUI = canCheckout && !purchaseMade;
        if (checkoutUIPrompt != null)
        {
            checkoutUIPrompt.SetActive(showCheckoutUI);
        }

        if (canCheckout && Input.GetKeyDown(KeyCode.Q))
        {
            Boleta();
            purchaseMade = true;
            if (checkoutUIPrompt != null) checkoutUIPrompt.SetActive(false);
            audioji.sfxSound.resource = audioji.buySound;
            audioji.sfxSound.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null && item.itemTemplate)
        {
            itemsInCart.Add(item.itemTemplate);
            Debug.Log($"Item agregado: {item.itemTemplate.itemName}");
        }

        if (other.CompareTag("Player"))
        {
            canCheckout = true;
            if (checkoutUIPrompt != null) checkoutUIPrompt.SetActive(true);

            Debug.Log("ola");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            itemsInCart.Remove(item.itemTemplate);
            Debug.Log($"Item quitado: {item.itemTemplate.itemName}");
        }
        if (other.CompareTag("Player"))
        {
            canCheckout = false;
            if (checkoutUIPrompt != null) checkoutUIPrompt.SetActive(false);
        }
    }
    public void Boleta()
    {
        string voucherString = string.Empty;
        float subtotalSinDescuento = 0f;
        float totalDescuentoAplicado = 0f;

        Dictionary<ItemTemplate, int> itemCounts = new Dictionary<ItemTemplate, int>();
        foreach (ItemTemplate item in itemsInCart)
        {
            if (itemCounts.ContainsKey(item)) itemCounts[item]++;
            else itemCounts.Add(item, 1);
        }
        voucherString += "--- **BOLETA** ---\n\n";

        foreach (var kvp in itemCounts)
        {
            ItemTemplate item = kvp.Key;
            int count = kvp.Value;
            float itemPrice = item.itemPrice;
            float totalItemPrice = itemPrice * count;
            float currentDiscount = 0f;

            switch (item.itemName)
            {
                case "Red Donut":
                    currentDiscount = totalItemPrice * 0.40f;
                    break;
                case "Red Apple":
                    currentDiscount = totalItemPrice * 0.50f;
                    break;

                case "Roll Toilet":
                    currentDiscount = totalItemPrice * 0.25f;
                    break;
            }

            subtotalSinDescuento += totalItemPrice;
            totalDescuentoAplicado += currentDiscount;

            string itemDetails = $"{item.itemName} x{count} ({item.itemType}) - Subtotal: ${totalItemPrice:F2}";
            if (currentDiscount > 0)
            {
                itemDetails += $" (Desc. -${currentDiscount:F2})";
            }
            voucherString += itemDetails + "\n";
        }

        float totalAPagar = subtotalSinDescuento - totalDescuentoAplicado;
        voucherString += "\n---------------------------\n";
        voucherString += $"**SUBTOTAL (Sin Desc.):** **${subtotalSinDescuento:F2}**\n";
        voucherString += $"**DESCUENTOS APLICADOS:** **-${totalDescuentoAplicado:F2}**\n";
        voucherString += "\n---------------------------\n";
        voucherString += $"**TOTAL FINAL A PAGAR:** **${totalAPagar:F2}**\n";
        if (boletaText != null)
        {
            boletaText.text = voucherString;
            boletaText.gameObject.SetActive(true);
        }

        lastPurchaseCorrect = VerifyShopping();
    }

    public bool VerifyShopping()
    {
        List<ItemTemplate> requiredList = listGenerator.shoppingList;
        List<ItemTemplate> purchasedList = itemsInCart;

        if (purchasedList.Count != requiredList.Count)
            return false;

        Dictionary<ItemTemplate, int> requiredCounts = new Dictionary<ItemTemplate, int>();
        foreach (ItemTemplate item in requiredList)
        {
            if (requiredCounts.ContainsKey(item)) requiredCounts[item]++;
            else requiredCounts.Add(item, 1);
        }

        Dictionary<ItemTemplate, int> purchasedCounts = new Dictionary<ItemTemplate, int>();
        foreach (ItemTemplate item in purchasedList)
        {
            if (purchasedCounts.ContainsKey(item)) purchasedCounts[item]++;
            else purchasedCounts.Add(item, 1);
        }

        if (requiredCounts.Count != purchasedCounts.Count) return false;

        foreach (var requiredKvp in requiredCounts)
        {
            ItemTemplate item = requiredKvp.Key;
            int requiredCount = requiredKvp.Value;

            if (!purchasedCounts.TryGetValue(item, out int purchasedCount) || purchasedCount != requiredCount)
            {
                return
                    false;
            }
        }

        return
            true;
    }
}

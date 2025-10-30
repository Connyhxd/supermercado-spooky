using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public List<ItemTemplate> itemsInCart = new List<ItemTemplate>();

    public ListGenerator listGenerator;

    public TextMeshProUGUI boletaText;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Boleta();
        }    
    }
    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null && item.itemTemplate && !itemsInCart.Contains(item.itemTemplate))
        {
            itemsInCart.Add(item.itemTemplate);
            CheckProgress();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null && itemsInCart.Contains(item.itemTemplate))
        {
            itemsInCart.Remove(item.itemTemplate);
            CheckProgress();
        }
    }

    void CheckProgress()
    {
        int correctItems = 0;

        foreach (ItemTemplate item in itemsInCart)
        {
            if (listGenerator.IsItemInShoppingList(item))
            {
                correctItems++;
            }
        }
    }

    void Boleta()
    {
        string voucherString = string.Empty;
        int totalCount = 0;
        float totalDiscount = 0;

        voucherString += "-- BOLETA --<br>";

        for (int i = 0; i < itemsInCart.Count; i++)
        {
            switch (itemsInCart[i].itemType)
            {
                case "Fruit":
                    totalDiscount += itemsInCart[i].itemPrice * 0.5f;
                    break;

                case "Vegetable":
                    break;
            }
            voucherString += itemsInCart[i].itemName + "-" + itemsInCart[i].itemType + "- $" + itemsInCart[i].itemPrice + "<br>";
            totalCount += itemsInCart[i].itemPrice;
        }

        voucherString += "-- TOTAL --<br>";
        voucherString += totalCount;
        voucherString += "<br>-- DESCUENTOS --<br>";
        voucherString += totalDiscount;
        voucherString += "<br>-- A PAGAR --<br>";
        voucherString += "$" + (totalCount - totalDiscount).ToString();

      if (boletaText != null)
       {
         boletaText.text = voucherString;
       }

        //Dictionary<ItemTemplate, int> itemCounts = new Dictionary<ItemTemplate, int>();

        //int total = 0;
        //string receipt = "BOLETA\n\n";

        //foreach (ItemTemplate item in itemsInCart)
        //{
        //    if (itemCounts.ContainsKey(item))
        //        itemCounts[item]++;
        //    else
        //        itemCounts[item] = 1;
        //}

        //foreach (KeyValuePair<ItemTemplate, int> kvp in itemCounts)
        //{
        //    ItemTemplate item = kvp.Key;
        //    int count = kvp.Value;
        //    int subtotal = item.itemPrice * count;

        //    receipt += item.itemName + " x" + count + " - $" + subtotal + "\n";
        //    total += subtotal;
        //}

        //receipt += "\nTOTAL: $" + total.ToString();

        //if (boletaText != null)
        //{
        //    boletaText.text = receipt;
        //}

    }

}

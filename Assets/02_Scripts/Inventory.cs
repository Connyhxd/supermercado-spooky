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
        if(Input.GetKeyDown(KeyCode.Q))
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

        voucherString += "-- BOLETA --\n\n";

        for (int i = 0; i < itemsInCart.Count; i++)
        {
            switch (itemsInCart[i].itemType)
            {
                case "Fruit":
                    totalDiscount += itemsInCart[i].itemPrice * 0.5f;
                    break;

                case "Vegetable":
                    break;

                case "Dairy":
                    break;
            }
            voucherString += itemsInCart[i].itemName + "-" + itemsInCart[i].itemType + "- $" + itemsInCart[i].itemPrice + "<br>";
            totalCount += itemsInCart[i].itemPrice;
        }

        voucherString += "\n-- TOTAL --\n";
        voucherString += totalCount;
        voucherString += "\n-- DESCUENTOS --\n";
        voucherString += totalDiscount;
        voucherString += "\n-- A PAGAR --\n";
        voucherString += "$" + (totalCount - totalDiscount).ToString();

      if (boletaText != null)
       {
         boletaText.text = voucherString;
       }

    }

}

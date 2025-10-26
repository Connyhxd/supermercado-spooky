using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public List<ItemTemplate> itemsInCart = new List<ItemTemplate>();

    public ListGenerator listGenerator;

    public TextMeshProUGUI boletaText;

    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null && item.itemTemplate && !itemsInCart.Contains(item.itemTemplate))
        {
            itemsInCart.Add(item.itemTemplate);
            CheckProgress();
            Boleta();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null && itemsInCart.Contains(item.itemTemplate))
        {
            itemsInCart.Remove(item.itemTemplate);
            CheckProgress();
            Boleta();
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

        Dictionary<ItemTemplate, int> itemCounts = new Dictionary<ItemTemplate, int>();

        int total = 0;
        string receipt = "BOLETA\n\n";

        foreach (ItemTemplate item in itemsInCart)
        {
            if (itemCounts.ContainsKey(item))
                itemCounts[item]++;
            else
                itemCounts[item] = 1;
        }

        foreach (KeyValuePair<ItemTemplate, int> kvp in itemCounts)
        {
            ItemTemplate item = kvp.Key;
            int count = kvp.Value;
            int subtotal = item.itemPrice * count;

            receipt += item.itemName + " x" + count + " - $" + subtotal + "\n";
            total += subtotal;
        }

        receipt += "\nTOTAL: $" + total.ToString();

        if (boletaText != null)
        {
            boletaText.text = receipt;
        }

    }

}

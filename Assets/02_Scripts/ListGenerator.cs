using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ListGenerator : MonoBehaviour
{
    public List<ItemTemplate> allItems = new List<ItemTemplate>();

    public List<ItemTemplate> shoppingList = new List<ItemTemplate>();

    public TMP_Text listText;

    private void Start()
    {
        GenerateList();
        ShowShoppingList();
    }

    void GenerateList()
    {
        int itemCount = Random.Range(8, 15);

        for (int i = 0; i < itemCount; i++)
        {
            int randomIndex = Random.Range(0, allItems.Count);
            ItemTemplate randomItem = allItems[randomIndex];

            shoppingList.Add(randomItem);
        }

    }


    void ShowShoppingList()
    {
        Dictionary<ItemTemplate, int> itemCounts = new Dictionary<ItemTemplate, int>();

        foreach (ItemTemplate item in shoppingList)
        {
            if (itemCounts.ContainsKey(item))
                itemCounts[item]++;
            else
                itemCounts[item] = 1;
        }

        string displayText = "Lista de compras:\n";

        foreach (KeyValuePair<ItemTemplate, int> entry in itemCounts)
        {
            if (entry.Value > 1)
                displayText += $"• {entry.Key.itemName} x{entry.Value}\n";
            else
                displayText += $"• {entry.Key.itemName}\n";
        }

        if (listText != null)
        {
            listText.text = displayText;
        }
    }

    public bool IsItemInShoppingList(ItemTemplate itemTemplate)
    {
        return shoppingList.Contains(itemTemplate);
    }

}

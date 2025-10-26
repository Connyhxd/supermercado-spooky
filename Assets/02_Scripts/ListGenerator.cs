using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ListGenerator : MonoBehaviour
{
    public List<string> allItems = new List<string>();

    public List<string> shoppingList = new List<string>();

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
            string randomItem = allItems[randomIndex];

            shoppingList.Add(randomItem);
        }

    }


    void ShowShoppingList()
    {
        Dictionary<string, int> itemCounts = new Dictionary<string, int>();

        foreach (string item in shoppingList)
        {
            if (itemCounts.ContainsKey(item))
                itemCounts[item]++;
            else
                itemCounts[item] = 1;
        }

        string displayText = "Lista de compras:\n\n";

        foreach (KeyValuePair<string, int> entry in itemCounts)
        {
            if (entry.Value > 1)
                displayText += $"• {entry.Key} x{entry.Value}\n";
            else
                displayText += $"• {entry.Key}\n";
        }

        if (listText != null)
        {
            listText.text = displayText;
        }
    }

}

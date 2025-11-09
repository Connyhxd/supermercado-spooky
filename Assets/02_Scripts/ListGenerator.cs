using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        string displayText = null;


        foreach (ItemTemplate item in shoppingList)
        {
            displayText += "• " + item.itemName + "\n";
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

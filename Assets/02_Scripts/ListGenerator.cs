using System.Collections.Generic;
using UnityEngine;

public class ListGenerator : MonoBehaviour
{
    public List<string> allItems = new List<string>();

    public List<string> shoppingList = new List<string>();

    private void Start()
    {
        GenerateList();
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

}

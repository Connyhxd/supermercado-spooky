using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> itemsInCart = new List<Item>();

    private ListGenerator listGenerator;

    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null && !itemsInCart.Contains(item))
        {
            itemsInCart.Add(item);
            CheckProgress();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null && itemsInCart.Contains(item))
        {
            itemsInCart.Remove(item);
            CheckProgress();
        }
    }

    private void CheckProgress()
    {
        int correctItems = 0;

        foreach (Item item in itemsInCart)
        {
            if (listGenerator.IsItemInShoppingList(item.itemTemplate))
            {
                correctItems++;
            }
        }
    }


}

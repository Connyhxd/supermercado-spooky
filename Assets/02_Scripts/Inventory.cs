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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null && itemsInCart.Contains(item))
        {
            itemsInCart.Remove(item);
        }
    }


}

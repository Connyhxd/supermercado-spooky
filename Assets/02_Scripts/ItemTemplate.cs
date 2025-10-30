using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "ScriptableObjects/Item")]
public class ItemTemplate : ScriptableObject
{
    public string itemName;
    public string itemType;
    public int itemPrice;
}

using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemSO itemSO;
    public string GetName() => itemSO.itemName;
    public Sprite GetIcon() => itemSO.icon;
}

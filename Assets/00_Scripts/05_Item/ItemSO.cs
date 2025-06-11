using UnityEngine;

public class ItemSO : ScriptableObject
{
    [Space(10)]
    [Header("Detail")]
    public int itemId = 0;
    public string name = "name";
    public Sprite sprite;
    public ItemType itemType;
    public int price = 1000;
}

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabaseSO", menuName = "ScriptableObject/Item/ItemDatabaseSO")]
public class ItemDatabaseSO : ScriptableObject
{
    public List<ItemSO> items;
}
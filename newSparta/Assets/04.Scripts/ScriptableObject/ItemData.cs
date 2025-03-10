using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipable,
    Cusmable,
    Resource
}

public enum ConsumableType
{
    Health,
    Hunger
}
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]

public class ItemData : ScriptableObject
{
   [Header("Info")]
   public string displayName;
   public string description;
   public ItemType type;
   public Sprite icon;
   public GameObject dropPrefab;
   [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;
    [Header("Cunsumable")]
    public ItemDataConsumable[] consumables;



}

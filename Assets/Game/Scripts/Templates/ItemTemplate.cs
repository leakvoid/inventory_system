using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Item Template", fileName = "New Item Template")]
public class ItemTemplate : ScriptableObject
{
    [field: SerializeField]
    public ItemTag Tag { get; private set; }
    [field: SerializeField, TextArea(3, 10)]
    public string Description { get; private set; }
    [field: SerializeField]
    public ItemType Type { get; private set; }
    [field: SerializeField]
    public Sprite Image { get; private set; }
}

public enum ItemType
{
    Material,
    Tool
}

public enum ItemTag
{
    Wood,
    Stone,
    Iron,
    Stick,
    Axe,
    Pickaxe,
    Hammer
}
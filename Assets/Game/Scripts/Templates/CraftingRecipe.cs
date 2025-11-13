using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Crafting Recipe", fileName = "New Crafting Recipe")]
public class CraftingRecipe : ScriptableObject
{
    [field: SerializeField]
    public int WoodRequired { get; private set; }
    [field: SerializeField]
    public int StoneRequired { get; private set; }
    [field: SerializeField]
    public int IronRequired { get; private set; }
    [field: SerializeField]
    public int StickRequired { get; private set; }
    [field: SerializeField]
    public ItemTemplate CraftingOutcome { get; private set; }
    [field: SerializeField]
    public int CraftedAmount { get; private set; }
}

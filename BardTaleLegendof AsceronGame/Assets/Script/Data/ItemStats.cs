using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStats : UnitBaseStats {
    public enum Rarity
    { Common, Uncommon, Rare, Mythical, Legendary, Relic}
    public enum Type
    { Equipment, Usable, Recipe, Miscellaneous, QuestItem}
    public int ItemLevel { get; set; }
    public int RequirementLevel { get; set; }
    public Rarity ItemRarity { get; set; }
    public Type ItemType { get; set; }
}

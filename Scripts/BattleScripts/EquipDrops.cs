using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment Drop List", menuName = "Equipment Drops")]
public class EquipDrops : ScriptableObject {

    public List<EquipableItem> Mythical;
    public List<EquipableItem> Rare;
    public List<EquipableItem> UnCommon;
    public List<EquipableItem> Common;

	public EquipableItem getEquip()
    {
        EquipableItem drop = null;

        string rarityDrop = whichRarity();
        List<EquipableItem> drops = (List<EquipableItem>)this.GetType().GetField(rarityDrop).GetValue(this);
        int dropCount = drops.Count;
        int randomDrop = Random.Range(0, dropCount);

        drop = drops[randomDrop];

        return drop;
    }

    private string whichRarity()
    {
        string rarity = null;

        int randomRoll = Random.Range(1, 1001);
        rarity = randomRoll >= 975 ? "Mythical" : randomRoll >= 900 ? "Rare" : randomRoll >= 800 ? "UnCommon" : "Common";

        return rarity;
    }
}

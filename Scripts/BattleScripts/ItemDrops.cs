using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Drop List", menuName = "Item Drops")]
public class ItemDrops : ScriptableObject {

    public List<UsableItem> Mythical;
    public List<UsableItem> Rare;
    public List<UsableItem> UnCommon;
    public List<UsableItem> Common;

    public UsableItem getItem()
    {
        UsableItem drop = null;

        string rarityDrop = whichRarity();
        List<UsableItem> drops = (List<UsableItem>)this.GetType().GetField(rarityDrop).GetValue(this);
        int dropCount = drops.Count;
        int randomDrop = Random.Range(0, dropCount);

        drop = drops[randomDrop];

        return drop;
    }

    private string whichRarity()
    {
        string rarity = null;

        int randomRoll = Random.Range(1, 101);
        rarity = randomRoll >= 90 ? "Mythical" : randomRoll >= 75 ? "Rare" : randomRoll >= 50 ? "UnCommon" : "Common";

        return rarity;
    }
}

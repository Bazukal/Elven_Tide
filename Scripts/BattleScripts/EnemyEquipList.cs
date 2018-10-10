using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipList", menuName = "Equip List")]
public class EnemyEquipList : ScriptableObject
{
    public List<EquipableItem> weapons;
    public List<EquipableItem> offHands;
    public List<EquipableItem> armors;
    public List<EquipableItem> accessories;

    private int level;

    private int getIndex()
    {
        int index = level <= 5 ? 0 : level <= 10 ? 1 : level <= 15 ? 2 : level <= 20 ? 3 :
            level <= 25 ? 4 : level <= 30 ? 5 : level <= 35 ? 6 : level <= 40 ? 7 :
            level <= 45 ? 8 : 9;

        return index;
    }

	public EquipableItem returnWeapon(int Level)
    {
        level = Level;

        int weapIndex = getIndex();
        return weapons[weapIndex];
    }

    public EquipableItem returnOffHand(int Level)
    {
        level = Level;
        int offHandIndex = getIndex();
        return offHands[offHandIndex];
    }

    public EquipableItem returnArmor(int Level)
    {
        level = Level;
        int armorIndex = getIndex();
        return armors[armorIndex];
    }

    public EquipableItem returnAccessory(int Level)
    {
        level = Level;
        int accessoryIndex = getIndex();
        return accessories[accessoryIndex];
    }
}

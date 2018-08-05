using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Text;

public class EquippedButtonEvent : MonoBehaviour {

    public static EquippedButtonEvent butEvent;

    private ScriptablePlayerClasses currentPlayer;

    public GameObject statCanvas;

    public Sprite sword;
    public Sprite dagger;
    public Sprite fist;
    public Sprite bow;
    public Sprite staff;
    public Sprite rod;
    public Sprite mace;
    public Sprite shieldOffHand;
    public Sprite emptyHanded;
    private Dictionary<string, Sprite> weapons = new Dictionary<string, Sprite>();

    public GameObject weaponBut;
    public GameObject offHandBut;
    public GameObject armorBut;
    public GameObject accBut;
    private Dictionary<string, GameObject> slotButtons = new Dictionary<string, GameObject>();

    public Text nameText;
    public Text damArmText;
    public Text statText;

    public UnityEvent onButtonHeld;
    private bool isButtonPressed = false;

    private Dictionary<string, EquipableItem> equippedItems;
    private string selectedItem;

	// Use this for initialization
	void Start () {
        butEvent = this;

        weapons.Add("Sword", sword);
        weapons.Add("Dagger", dagger);
        weapons.Add("Fist", fist);
        weapons.Add("Bow", bow);
        weapons.Add("Mace", mace);
        weapons.Add("Staff", staff);
        weapons.Add("Rod", rod);

        slotButtons.Add("Weapon", weaponBut);
        slotButtons.Add("OffHand", offHandBut);
        slotButtons.Add("Armor", armorBut);
        slotButtons.Add("Accessory", accBut);

        float height = Screen.height * 0.25f;
        float width = Screen.width * 0.2f;

        Debug.Log(string.Format("Height of the Current Screen: {0}.\nWidth of the Current Screen: {1}.\nOne Quarter Height of the Current Screen is: {2}.\nOne Quarter Width of the Current Screen is: {3}.", Screen.height, Screen.width, height, width));

        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
	}

    private void Update()
    {
        if (isButtonPressed)
            onButtonHeld.Invoke();
    }

    public void ButtonPressed(string buttonValue)
    {
        isButtonPressed = true;
        selectedItem = buttonValue;
    }

    public void ButtonReleased()
    {
        isButtonPressed = false;
        statCanvas.GetComponent<RectTransform>().localPosition = new Vector2(3000, 3000);
    }

    public void SetButtonSprites()
    {
        currentPlayer = StatsScreen.stats.GetPlayerObject();

        equippedItems = new Dictionary<string, EquipableItem>();
        equippedItems.Add("Weapon", currentPlayer.weapon);
        equippedItems.Add("OffHand", currentPlayer.offHand);
        equippedItems.Add("Armor", currentPlayer.armor);
        equippedItems.Add("Accessory", currentPlayer.accessory);

        weaponBut.GetComponent<Image>().sprite = weapons[equippedItems["Weapon"].equipType];

        try
        {
            string whatType = equippedItems["OffHand"].type;

            if (whatType.Equals("Armor"))
                offHandBut.GetComponent<Image>().sprite = shieldOffHand;
            else
                offHandBut.GetComponent<Image>().sprite = weapons[equippedItems["OffHand"].equipType];
        }
        catch { offHandBut.GetComponent<Image>().sprite = emptyHanded; }        
    }

    public void DisplayStats()
    {
        EquipableItem equip = equippedItems[selectedItem];
        if(equip != null)
        {
            nameText.text = equip.name;

            if (selectedItem.Equals("Weapon"))
                damArmText.text = string.Format("Damage: {0}", equip.currentDamage);
            else if (selectedItem.Equals("Armor"))
                damArmText.text = string.Format("Armor: {0}", equip.currentArmor);
            else if (selectedItem.Equals("OffHand"))
            {
                if (equip.type.Equals("Weapon"))
                    damArmText.text = string.Format("Damage: {0}", equip.currentDamage);
                else
                    damArmText.text = string.Format("Armor: {0}", equip.currentArmor);
            }
            else if (selectedItem.Equals("Accessory"))
            {
                if (equip.currentDamage > 0)
                    damArmText.text = string.Format("Damage: {0}", equip.currentDamage);
                else
                    damArmText.text = string.Format("Armor: {0}", equip.currentArmor);
            }

            StringBuilder stats = new StringBuilder();

            int str = equip.currentStrength;
            int agi = equip.currentAgility;
            int mind = equip.currentMind;
            int soul = equip.currentSoul;

            if (str > 0)
                stats.Append(string.Format("Strength: {0}\n", str));
            if (agi > 0)
                stats.Append(string.Format("Agility: {0}\n", agi));
            if (mind > 0)
                stats.Append(string.Format("Mind: {0}\n", mind));
            if (soul > 0)
                stats.Append(string.Format("Soul: {0}\n", soul));

            statText.text = stats.ToString();

            statCanvas.transform.SetParent(slotButtons[selectedItem].transform, false);

            float width = Screen.width * 0.2f;
            float position = (width / 2) + (width * 0.1f);
            statCanvas.GetComponent<RectTransform>().localPosition = new Vector2(position, 0);
        }        
    }
}

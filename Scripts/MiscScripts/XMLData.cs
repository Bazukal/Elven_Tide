using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class XMLData : MonoBehaviour {

    public const string ITEMPATH = "items";
    public const string QUESTPATH = "quests";
    public const string CHATPATH = "chats";
    public const string SKILLPATH = "skills";
    public const string STATPATH = "levelStats";
    
    private List<EquipableItemClass> equipableItems = new List<EquipableItemClass>();
    private List<UsableItemClass> usableItems = new List<UsableItemClass>();

    private List<ChatClass> citizen1 = new List<ChatClass>();
    private List<ChatClass> citizen2 = new List<ChatClass>();
    private List<ChatClass> citizen3 = new List<ChatClass>();
    private List<ChatClass> citizen4 = new List<ChatClass>();

    private List<QuestClass> allQuests = new List<QuestClass>();

    private List<SkillClass> enemySkills = new List<SkillClass>();
    private List<SkillClass> archerSkills = new List<SkillClass>();
    private List<SkillClass> blackSkills = new List<SkillClass>();
    private List<SkillClass> monkSkills = new List<SkillClass>();
    private List<SkillClass> paladinSkills = new List<SkillClass>();
    private List<SkillClass> thiefSkills = new List<SkillClass>();
    private List<SkillClass> warriorSkills = new List<SkillClass>();
    private List<SkillClass> whiteSkills = new List<SkillClass>();


    public Slider loadingSlider;
    public Text progressText;

    // Use this for initialization
    void Start () {
        
        SetChats();

        SetItems();

        SetQuests();

        SetSkills();

        SetStats();

        StartCoroutine(loadXML());
	}

    private void SetChats()
    {
        //Reading npc chats from XML file, and storing in manager
        ChatClass newChat;

        ChatContainer cc = ChatContainer.Load(CHATPATH);

        foreach (Chat chat in cc.chats)
        {
            newChat = new ChatClass(chat.character, chat.questStage, chat.chatString);

            string citi = chat.character;

            switch (citi)
            {
                case "Citizen1":
                    citizen1.Add(newChat);
                    break;
                case "Citizen2":
                    citizen2.Add(newChat);
                    break;
                case "Citizen3":
                    citizen3.Add(newChat);
                    break;
                case "Citizen4":
                    citizen4.Add(newChat);
                    break;
            }
        }
        GameChats.gChats.setAllChats(citizen1, citizen2, citizen3, citizen4);
    }

    private void SetItems()
    {
        //reads items from xml file, and stores into lists
        UsableItemClass newUsable;
        EquipableItemClass newEquipable;

        ItemContainer ic = ItemContainer.Load(ITEMPATH);

        foreach (Item item in ic.items)
        {
            string typeCheck = item.type;

            switch (typeCheck)
            {
                case "Weapon":
                case "Armor":
                case "Accessory":
                    newEquipable = new EquipableItemClass(item.name, item.type, item.equipable, item.usable, item.revive,
                        item.minLevel, item.maxLevel, item.boughtOrDrop, item.buyPrice, item.sellPrice, 1, item.weaponType,
                        item.armorType, item.damage, item.armor, item.strength, item.agility, item.mind, item.soul);

                    equipableItems.Add(newEquipable);
                    break;
                case "Usable":
                    newUsable = new UsableItemClass(item.name, item.type, item.equipable, item.usable, item.revive,
                        item.minLevel, item.maxLevel, item.boughtOrDrop, item.buyPrice, item.sellPrice, 1, item.cureAilment,
                        item.healAmount);

                    usableItems.Add(newUsable);
                    break;
            }
        }

        GameItems.gItems.setItems(equipableItems, usableItems);
    }

    private void SetQuests()
    {
        //Reading Quests from XML file, and storing in manager
        QuestClass newQuest;

        QuestContainer qc = QuestContainer.Load(QUESTPATH);

        foreach (Quest quest in qc.quests)
        {
            string qType = quest.questType;

            switch (qType)
            {
                case "Find":
                    newQuest = new QuestClass(quest.questName, quest.questGiver, quest.questType, quest.find,
                        quest.startScript, quest.progressChat, quest.finishScript, quest.questDesc, quest.stage);

                    allQuests.Add(newQuest);
                    break;
                case "Kill":
                    newQuest = new QuestClass(quest.questName, quest.questGiver, quest.questType, quest.killName,
                        quest.startScript, quest.progressChat, quest.finishScript, quest.questDesc, quest.stage, quest.killAmount);

                    allQuests.Add(newQuest);
                    break;
                case "Collect":
                    newQuest = new QuestClass(quest.questName, quest.questGiver, quest.questType, quest.itemCollect,
                        quest.collectWhere, quest.startScript, quest.progressChat, quest.finishScript, quest.questDesc,
                        quest.stage, quest.collectAmount);

                    allQuests.Add(newQuest);
                    break;
            }
        }
        QuestListing.qListing.setQuests(allQuests);
    }

    private void SetSkills()
    {
        //reading skills from XML, and store in manager
        SkillClass newSkill;

        SkillContainer sc = SkillContainer.Load(SKILLPATH);

        foreach (Skill skill in sc.skills)
        {
            string charClass = skill.charClass;

            newSkill = new SkillClass(skill.name, skill.charClass, skill.type, skill.damageType, 
                skill.debuffType, skill.target, skill.desc, skill.cure, skill.stat, skill.debuffChance,
                skill.strength, skill.plus, skill.turns, skill.level, skill.mana, skill.aoe);

            switch(charClass)
            {
                case "Enemy":
                    enemySkills.Add(newSkill);
                    break;
                case "Archer":
                    archerSkills.Add(newSkill);
                    break;
                case "Black Mage":
                    blackSkills.Add(newSkill);
                    break;
                case "Monk":
                    monkSkills.Add(newSkill);
                    break;
                case "Paladin":
                    paladinSkills.Add(newSkill);
                    break;
                case "Thief":
                    thiefSkills.Add(newSkill);
                    break;
                case "Warrior":
                    warriorSkills.Add(newSkill);
                    break;
                case "White Mage":
                    whiteSkills.Add(newSkill);
                    break;
            }
        }

        GameSkills.skills.SetSkills(enemySkills, archerSkills, blackSkills, monkSkills, paladinSkills, thiefSkills,
            warriorSkills, whiteSkills);
    }

    private void SetStats()
    {
        StatClass newStats;
        LevelStatContainer lc = LevelStatContainer.Load(STATPATH);
        foreach(Stat stats in lc.stats)
        {
            string charClass = stats.charClass;
            newStats = new StatClass(stats.hpMinValue, stats.hpMaxValue, stats.mpMinValue, stats.mpMaxValue,
                stats.strMinValue, stats.strMaxValue, stats.agiMinValue, stats.agiMaxValue, stats.mindMinValue,
                stats.mindMaxValue, stats.soulMinValue, stats.soulMaxValue, stats.defMinValue, stats.defMaxValue);
            
            LevelGrowth.growth.addDictionary(charClass, newStats);
        }
    }

    //loads xml files, while displaying a loading bar during the process
    IEnumerator loadXML()
    {       
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("TitleScreen");

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            loadingSlider.value = progress;
            progressText.text = progress * 100f + "%";

            yield return null;
        }
    }
}

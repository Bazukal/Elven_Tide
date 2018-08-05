using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCharSprite : MonoBehaviour {

    public static SetCharSprite sprites;

    public List<Sprite> characterSprites;
    public List<RuntimeAnimatorController> classAnimations;
    private List<string> classes = new List<string>() { "Archer", "Black Mage", "Monk", "Paladin", "Thief", "Warrior", "White Mage" };

    private Dictionary<string, Sprite> spriteDict = new Dictionary<string, Sprite>();
    private Dictionary<string, RuntimeAnimatorController> animationDict = new Dictionary<string, RuntimeAnimatorController>();

	// Use this for initialization
	void Start () {
        sprites = this;

        for(int i = 0;i < classes.Count;i++)
        {
            spriteDict.Add(classes[i], characterSprites[i]);
            animationDict.Add(classes[i], classAnimations[i]);
        }

        SetCharacter();
	}
	
	public void SetCharacter()
    {
        ScriptablePlayerClasses p1 = Manager.manager.GetPlayer("Player1");
        ScriptablePlayerClasses p2 = Manager.manager.GetPlayer("Player2");
        ScriptablePlayerClasses p3 = Manager.manager.GetPlayer("Player3");
        ScriptablePlayerClasses p4 = Manager.manager.GetPlayer("Player4");

        string playerClass = null;

        if (p1.currentHp > 0)
            playerClass = p1.charClass;
        else if (p2.currentHp > 0)
            playerClass = p2.charClass;
        else if (p3.currentHp > 0)
            playerClass = p3.charClass;
        else if (p4.currentHp > 0)
            playerClass = p4.charClass;

        Debug.Log(playerClass);
        GetComponent<SpriteRenderer>().sprite = spriteDict[playerClass];
        GetComponent<Animator>().runtimeAnimatorController = animationDict[playerClass];
    }
}

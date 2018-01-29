using System.Xml.Serialization;
using System.Xml;


public class Quest {

    [XmlAttribute("Name")]
    public string questName;

    [XmlElement("Stage")]
    public int stage;

    [XmlElement("QuestGiver")]
    public string questGiver;

    [XmlElement("QuestPart")]
    public int questPart;

    [XmlElement("QuestType")]
    public string questType;

    [XmlElement("Find")]
    public string find;

    [XmlElement("KillName")]
    public string killName;

    [XmlElement("KillAmount")]
    public int killAmount;

    [XmlElement("ItemCollect")]
    public string itemCollect;

    [XmlElement("CollectAmount")]
    public int collectAmount;

    [XmlElement("CollectWhere")]
    public string collectWhere;

    [XmlElement("StartScript")]
    public string startScript;

    [XmlElement("InProgressChat")]
    public string progressChat;

    [XmlElement("FinishScript")]
    public string finishScript;

    [XmlElement("QuestDescription")]
    public string questDesc;
}

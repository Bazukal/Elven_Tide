using System.Xml.Serialization;
using System.Xml;

public class Chat {

    [XmlAttribute("Character")]
    public string character;

    [XmlElement("QuestStage")]
    public int questStage;

    [XmlElement("ChatString")]
    public string chatString;
	
}

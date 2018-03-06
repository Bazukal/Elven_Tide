using System.Xml.Serialization;
using System.Xml;

public class Stat {

    [XmlAttribute("CharClass")]
    public string charClass;

    [XmlElement("HPMinValue")]
    public int hpMinValue;

    [XmlElement("HPMaxValue")]
    public int hpMaxValue;

    [XmlElement("MPMinValue")]
    public int mpMinValue;

    [XmlElement("MPMaxValue")]
    public int mpMaxValue;

    [XmlElement("StrMinValue")]
    public int strMinValue;

    [XmlElement("StrMaxValue")]
    public int strMaxValue;

    [XmlElement("AgiMinValue")]
    public int agiMinValue;

    [XmlElement("AgiMaxValue")]
    public int agiMaxValue;

    [XmlElement("MindMinValue")]
    public int mindMinValue;

    [XmlElement("MindMaxValue")]
    public int mindMaxValue;

    [XmlElement("SoulMinValue")]
    public int soulMinValue;

    [XmlElement("SoulMaxValue")]
    public int soulMaxValue;

    [XmlElement("DefMinValue")]
    public int defMinValue;

    [XmlElement("DefMaxValue")]
    public int defMaxValue;
}

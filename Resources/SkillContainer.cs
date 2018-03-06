using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("SkillCollection")]
public class SkillContainer{

    [XmlArray("Skills")]
    [XmlArrayItem("Skill")]
    public List<Skill> skills = new List<Skill>();

    public static SkillContainer Load(string path)
    {
        TextAsset _xml = Resources.Load<TextAsset>(path);
        XmlSerializer serializer = new XmlSerializer(typeof(SkillContainer));

        StringReader reader = new StringReader(_xml.text);

        SkillContainer skills = serializer.Deserialize(reader) as SkillContainer;

        reader.Close();

        return skills;
    }
}

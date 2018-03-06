using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("StatCollection")]

public class LevelStatContainer {

    [XmlArray("Stats")]
    [XmlArrayItem("Stat")]
    public List<Stat> stats = new List<Stat>();

    public static LevelStatContainer Load(string path)
    {
        TextAsset _xml = Resources.Load<TextAsset>(path);
        
        XmlSerializer serializer = new XmlSerializer(typeof(LevelStatContainer));        
        StringReader reader = new StringReader(_xml.text);
        
        LevelStatContainer stats = serializer.Deserialize(reader) as LevelStatContainer;
        
        reader.Close();
        
        return stats;
    }
}
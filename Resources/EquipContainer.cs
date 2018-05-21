using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("EquipCollection")]
public class EquipContainer {

    [XmlArray("Equips")]
    [XmlArrayItem("Equip")]
    public List<Equip> equips = new List<Equip>();

    public static EquipContainer Load(string path)
    {
        TextAsset _xml = Resources.Load<TextAsset>(path);
        XmlSerializer serializer = new XmlSerializer(typeof(EquipContainer));

        StringReader reader = new StringReader(_xml.text);

        EquipContainer equips = serializer.Deserialize(reader) as EquipContainer;

        reader.Close();

        return equips;
    }
}

using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("CharacterCollection")]
public class CharacterContainer {

    [XmlArray("Characters")]
    [XmlArrayItem("Character")]
    public List<Character> characters = new List<Character>();

    public static CharacterContainer Load(string path)
    {
        TextAsset _xml = Resources.Load<TextAsset>(path);
        XmlSerializer serializer = new XmlSerializer(typeof(CharacterContainer));

        StringReader reader = new StringReader(_xml.text);

        CharacterContainer characters = serializer.Deserialize(reader) as CharacterContainer;

        reader.Close();

        return characters;
    }        
}

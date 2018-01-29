using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("ChatCollection")]
public class ChatContainer {

    [XmlArray("Chats")]
    [XmlArrayItem("Chat")]
    public List<Chat> chats = new List<Chat>();

    public static ChatContainer Load(string path)
    {
        TextAsset _xml = Resources.Load<TextAsset>(path);
        XmlSerializer serializer = new XmlSerializer(typeof(ChatContainer));

        StringReader reader = new StringReader(_xml.text);

        ChatContainer chats = serializer.Deserialize(reader) as ChatContainer;

        reader.Close();

        return chats;
    }

}

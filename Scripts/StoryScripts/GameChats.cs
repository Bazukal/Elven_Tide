using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//holds all npc chats in game
public class GameChats : MonoBehaviour {

    public static GameChats gChats;

    private List<ChatClass> citizen1 = new List<ChatClass>();
    private List<ChatClass> citizen2 = new List<ChatClass>();
    private List<ChatClass> citizen3 = new List<ChatClass>();
    private List<ChatClass> citizen4 = new List<ChatClass>();

    private void Awake()
    {
        gChats = this;
    }

    public void setAllChats(List<ChatClass> citi1, List<ChatClass> citi2, List<ChatClass> citi3, List<ChatClass> citi4)
    {
        citizen1 = citi1;
        citizen2 = citi2;
        citizen3 = citi3;
        citizen4 = citi4;
    }

    public List<ChatClass> getCitizen1() { return citizen1; }
    public List<ChatClass> getCitizen2() { return citizen2; }
    public List<ChatClass> getCitizen3() { return citizen3; }
    public List<ChatClass> getCitizen4() { return citizen4; }
}

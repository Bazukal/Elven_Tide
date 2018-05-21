using UnityEngine;

public class CloseNpcPanel : MonoBehaviour {

    public static CloseNpcPanel closeNpcPanel;

    private void Awake()
    {
        closeNpcPanel = this;
    }

    //turns on or off npc ui panel/turns off player ui
    public void activatePanel()
    {
        if (gameObject.GetComponent<CanvasGroup>().alpha == 1)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.GetComponent<CanvasGroup>().interactable = false;
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }            
        else
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
            gameObject.GetComponent<CanvasGroup>().interactable = true;
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
}

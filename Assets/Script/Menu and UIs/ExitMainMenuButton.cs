using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;    

public class ExitMainMenuButton : MonoBehaviour
{
    [SerializeField] Sprite clickedButtonSprite;
    [SerializeField] Sprite originalButtonSprite;
    [SerializeField] Button button;
    [SerializeField] GameObject exitMsgPanel;

    void Update()
    {
        IfPopUpIsActive();
    }

    public void ChangeButtonSprite()
    {
        button.image.sprite = clickedButtonSprite;

    }

    private void IfPopUpIsActive()
    {
        if (!exitMsgPanel.activeSelf)
        {
            button.image.sprite = originalButtonSprite;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManiMenu : MonoBehaviour
{
    [SerializeField] Sprite clickedButtonSprite;
    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI buttonText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeButtonSprite()
    {
        button.image.sprite = clickedButtonSprite;
        buttonText.rectTransform.anchoredPosition = button.image.sprite.rect.position;
        Debug.Log(buttonText.rectTransform.anchoredPosition);
        buttonText.text = "Play";
        
    }

}

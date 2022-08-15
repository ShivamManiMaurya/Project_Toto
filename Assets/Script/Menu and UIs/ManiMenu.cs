using UnityEngine;
using UnityEngine.UI;

public class ManiMenu : MonoBehaviour
{
    [SerializeField] Sprite clickedButtonSprite;
    [SerializeField] Button button;

    public void ChangeButtonSprite()
    {
        button.image.sprite = clickedButtonSprite;
    }


}

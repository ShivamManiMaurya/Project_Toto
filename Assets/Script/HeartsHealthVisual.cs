using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsHealthVisual : MonoBehaviour
{
    [SerializeField] private Sprite heart0Sprite;
    [SerializeField] private Sprite heart1Sprite;
    [SerializeField] private Sprite heart2Sprite;
    [SerializeField] private Sprite heart3Sprite;
    [SerializeField] private Sprite heart4Sprite;

    private List<HeartImage> heartImageList;

    private void Start()
    {
        MakeAndReturnHeartImage(new Vector2(0, 0));
        MakeAndReturnHeartImage(new Vector2(30, 0));
        MakeAndReturnHeartImage(new Vector2(60, 0));
    }

    private HeartImage MakeAndReturnHeartImage(Vector2 anchoredPosition)
    {
        // make gameobject
        GameObject heartGameObject = new GameObject("Heart", typeof(Image));

        // set as child of this transform
        heartGameObject.transform.parent = transform;
        heartGameObject.transform.localPosition = Vector3.zero;

        // Locate and Size heart
        heartGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        heartGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(26f, 26f);

        // set heart sprite
        Image heartImageUI = heartGameObject.GetComponent<Image>();
        heartImageUI.sprite = heart0Sprite;

        // HeartImage class Obejct is made so Constructor with one variable is called
        HeartImage heartImage = new HeartImage(heartImageUI);
        heartImageList.Add(heartImage);


        return heartImage;

    }


    // Represent a single heart
    public class HeartImage
    {
        private Image heartImage;

        // Constructor is made
        public HeartImage(Image heartImage)
        {
            this.heartImage = heartImage;
        }

    }
}

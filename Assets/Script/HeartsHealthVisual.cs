using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HeartsHealthVisual : MonoBehaviour
{
    public static HeartsHealthSystem heartsHealthSystemStatic;

    [SerializeField] private Sprite heart0Sprite;
    [SerializeField] private Sprite heart1Sprite;
    [SerializeField] private Sprite heart2Sprite;
    [SerializeField] private Sprite heart3Sprite;
    [SerializeField] private Sprite heart4Sprite;

    private List<HeartImage> heartImageList;
    private HeartsHealthSystem heartsHealthSystem;

    // my change
    public bool isDead = false;
    // *********

    private void Awake()
    {
        heartImageList = new List<HeartImage>();
    }


    private void Start()
    {
        HeartsHealthSystem heartsHealthSystem = new HeartsHealthSystem(4);
        SetHeartHealthSystem(heartsHealthSystem);
    }

  
    public void SetHeartHealthSystem(HeartsHealthSystem heartsHealthSystem)
    {
        this.heartsHealthSystem = heartsHealthSystem;
        heartsHealthSystemStatic = heartsHealthSystem;

        List<HeartsHealthSystem.Heart> heartList = heartsHealthSystem.GetHeartList();

        Vector2 heartAnchoredPosition = new Vector2(0, 0);
        for (int i = 0; i < heartList.Count; i++)
        {
            HeartsHealthSystem.Heart heart = heartList[i];
            CreateHeartImage(heartAnchoredPosition).SetHeartFragments(heart.GetFragmentAmount());
            heartAnchoredPosition += new Vector2(30, 0);
        }

        heartsHealthSystem.OnDamaged += HeartsHealthSystem_OnDamaged;
        heartsHealthSystem.OnHealed += HeartsHealthSystem_OnHealed;
        heartsHealthSystem.OnDead += HeartsHealthSystem_OnDead;
    }

    private void HeartsHealthSystem_OnDead(object sender, System.EventArgs e)
    {
        Debug.Log("Dead!");
        isDead = true;
    }

    private void HeartsHealthSystem_OnHealed(object sender, System.EventArgs e)
    {
        // Hearts health system was healed
        RefreshAllHearts();
    }

    private void HeartsHealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        // Hearts health system was damaged
        RefreshAllHearts();
    }

    private void RefreshAllHearts()
    {
        List<HeartsHealthSystem.Heart> heartList = heartsHealthSystem.GetHeartList();
        for (int i = 0; i < heartImageList.Count; i++)
        {
            HeartImage heartImage = heartImageList[i];
            HeartsHealthSystem.Heart heart = heartList[i];
            heartImage.SetHeartFragments(heart.GetFragmentAmount());
        }
    }

    private HeartImage CreateHeartImage(Vector2 anchoredPosition)
    {
        // make gameobject
        GameObject heartGameObject = new GameObject("Heart", typeof(Image));

        // set as child of this transform
        heartGameObject.transform.SetParent(transform);

        //heartGameObject.transform.parent = transform;
        heartGameObject.transform.localPosition = Vector3.zero;

        // Locate and Size heart
        heartGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        heartGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(26f, 26f);

        // set heart sprite
        Image heartImageUI = heartGameObject.GetComponent<Image>();
        heartImageUI.sprite = heart4Sprite;

        // HeartImage class Obejct is made so Constructor with one variable is called
        HeartImage heartImage = new HeartImage(this, heartImageUI);
        heartImageList.Add(heartImage);

        return heartImage;
    }

    // Represent a single heart
    public class HeartImage
    {
        private int fragments;
        private Image heartImage;
        private HeartsHealthVisual heartsHealthVisual;

        // Constructor is made
        public HeartImage(HeartsHealthVisual heartsHealthVisual, Image heartImage)
        {
            this.heartsHealthVisual = heartsHealthVisual;
            this.heartImage = heartImage;
        }

        public void SetHeartFragments(int fragments)
        {
            this.fragments = fragments;
            switch (fragments)
            {
                case 0: heartImage.sprite = heartsHealthVisual.heart0Sprite; break;
                case 1: heartImage.sprite = heartsHealthVisual.heart1Sprite; break;
                case 2: heartImage.sprite = heartsHealthVisual.heart2Sprite; break;
                case 3: heartImage.sprite = heartsHealthVisual.heart3Sprite; break;
                case 4: heartImage.sprite = heartsHealthVisual.heart4Sprite; break;
            }

        }

    }
}

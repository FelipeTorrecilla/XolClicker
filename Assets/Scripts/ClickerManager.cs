using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickerManager : MonoBehaviour
{
    
    public TMP_Text lotlsUI;
    public TMP_Text axolotlLevelText;
    public TMP_Text autoclickerText;
    public TMP_Text multiplierText;
    public TMP_Text pointsEarnedText;
    public ShopItemSO[] shopItemsSO;
    public GameObject[] shopPanelsGO;
    public ShopTemplate[] shopPanels;
    public Button[] myPurchaseBtns;
    public GameObject frendObject;
    public GameObject multiplierObject;

    public AudioSource clickSound;
    public AudioClip[] clickSounds;

    public int lotls;
    private int upgradeLevel = 1;
    private int multiplierUpgradeLevel = 1;

    private int autoclickerLevel = 0;
    private int autoclickerBasePoints = 1;
    private float autoclickerInterval = 1f;
    private float autoclickerTimer = 0f;

    public GameObject pointsEarnedObject;
    public float pointsDisplayDuration = 2f;
    private float pointsDisplayTimer = 0f;
    private bool isDisplayingPoints = false;

    
    

    void Start()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
            shopPanelsGO[i].SetActive(true);
        lotlsUI.text = "Lotls: " + lotls.ToString();
        axolotlLevelText.text = "Axolotl Level: " + upgradeLevel.ToString();
        LoadPanels();
        CheckPurchaseable();
    }

    void Update()
    {
        AutoclickerTimerUpdate();

        if (isDisplayingPoints)
        {
            pointsDisplayTimer += Time.deltaTime;
            if (pointsDisplayTimer >= pointsDisplayDuration)
            {
                HidePointsEarned();
            }
        }
    }

//Clicker

    public void AddLotls()
    {
        lotls += upgradeLevel * multiplierUpgradeLevel;
        lotlsUI.text = "Lotls: " + lotls.ToString();
        PlayRandomClickSound();
        CheckPurchaseable();
    }

    private void PlayRandomClickSound()
    {
        if (clickSounds.Length == 0)
            return;

        int randomIndex = Random.Range(0, clickSounds.Length);
        AudioClip randomClip = clickSounds[randomIndex];

        clickSound.pitch = Random.Range(0.85f, 1.15f); 
        clickSound.PlayOneShot(randomClip);
    }

//Autoclicker

    private void AutoclickerTimerUpdate()
    {
        autoclickerTimer += Time.deltaTime;
        if (autoclickerTimer >= autoclickerInterval)
        {
            autoclickerTimer -= autoclickerInterval;
            GenerateAutoclickerPoints();
        }
    }

    private void GenerateAutoclickerPoints()
    {
        int autoclickerPoints = autoclickerLevel * multiplierUpgradeLevel;
        lotls += autoclickerPoints;
        lotlsUI.text = "Lotls: " + lotls.ToString();
        DisplayPointsEarned(autoclickerPoints);
        CheckPurchaseable();
    }

//Display Points

    private void DisplayPointsEarned(int points)
    {
        if(autoclickerLevel >= 1)
        {
            pointsEarnedText.text = "+" + points.ToString() + " Lotls";
            pointsEarnedObject.SetActive(true);
            isDisplayingPoints = true;
            pointsDisplayTimer = 0f;
        }    
    }

    private void HidePointsEarned()
    {
        pointsEarnedObject.SetActive(false);
        isDisplayingPoints = false;
    }

//Shop

    public void CheckPurchaseable()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            if (lotls >= shopItemsSO[i].baseCost)
                myPurchaseBtns[i].interactable = true;
            else
                myPurchaseBtns[i].interactable = false;                
        }
    }

    public void PurchaseItem(int btnNo)
    {
        if (lotls >= shopItemsSO[btnNo].baseCost)
        {
            lotls = lotls - shopItemsSO[btnNo].baseCost;
            lotlsUI.text = "Lotls: " + lotls.ToString();
                        
            upgradeLevel += shopItemsSO[btnNo].baseUpgrade;
            autoclickerLevel += shopItemsSO[btnNo].baseAutoUpgrade;
            multiplierUpgradeLevel += shopItemsSO[btnNo].baseMultiplierUpgrade;

            axolotlLevelText.text = "Axolotl Level: " + upgradeLevel.ToString();          
           
            if (autoclickerLevel >= 1)
            {
                frendObject.SetActive(true);
                autoclickerText.text = "Alotl Level: " + autoclickerLevel.ToString();
            }

            if (multiplierUpgradeLevel >= 2)
            {
                multiplierObject.SetActive(true);
                multiplierText.text = "Xol Level: " + multiplierUpgradeLevel.ToString();
            }
            
            CheckPurchaseable();
        }
    }

    public void LoadPanels()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            shopPanels[i].titleTxt.text = shopItemsSO[i].title;
            shopPanels[i].descriptionTxt.text = shopItemsSO[i].description;
            shopPanels[i].logoImg.sprite = shopItemsSO[i].newSprite;
            shopPanels[i].costTxt.text = "Lotls: " + shopItemsSO[i].baseCost.ToString();
        }
    }
}

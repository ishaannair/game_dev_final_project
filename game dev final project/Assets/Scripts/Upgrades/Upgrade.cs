using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public string upgradeName;
    public Button upgradeButton;
    public Image upgradeIcon;
    public Sprite tickMark;
    public GameConstants gameConstants;

    public Text scrapCounter;


    void Start(){
        bool upgradeCheck = (bool)gameConstants.GetType().GetField(upgradeName).GetValue(gameConstants);
        // Debug.Log(upgradeCheck);
        if (upgradeCheck){
            upgradeButton.enabled = false;
            upgradeIcon.sprite = tickMark;
        }
    }

    public void purchaseUpgrade(){
        if (gameConstants.totalScrap - gameConstants.level1UpgradeScrapCost >= 0){
            bool upgradeCheck = (bool)gameConstants.GetType().GetField(upgradeName).GetValue(gameConstants);
            if (!upgradeCheck){
                UpdateUpgrade(upgradeName);
            }
        }
    }

    public void UpdateUpgrade(string upgradeName){
        gameConstants.GetType().GetField(upgradeName).SetValue(gameConstants,true);
        UpdateStats(upgradeName);
        upgradeButton.enabled = false;
        upgradeIcon.sprite = tickMark;
        gameConstants.totalScrap -= gameConstants.level1UpgradeScrapCost;
        // Debug.Log(gameConstants.totalScrap);
        scrapCounter.text = gameConstants.totalScrap.ToString();
    }

    public void UpdateStats(string upgradeName){
        switch (upgradeName){
            case "helmetUpgradeResource":
                gameConstants.scrapRate *= 2;
                break;
            case "helmetUpgradeDamage":
                gameConstants.blasterDamage *= 1.2f;
                gameConstants.shotgunDamage *= 1.2f;
                gameConstants.rocketDamage *= 1.2f;
                break;
            case "helmetUpgradeReload":
                gameConstants.blasterReloadTime *= 0.8f;
                gameConstants.shotgunReloadTime *= 0.8f;
                gameConstants.rocketReloadTime *= 0.8f;
                break;
            
            case "torsoUpgradeHP":
                gameConstants.startingTimer += 100;
                break;
            case "torsoUpgradeDecay":
                gameConstants.startingDecay *= 0.75f;
                break;
            case "torsoUpgradeSpeed":
                gameConstants.playerSpeed += 2;
                break;
            
            case "gloveUpgradeAtkSpd":
                gameConstants.playerSlashDuration *= 0.8f;
                break;
            // case "gloveUpgradeDblDmg":
            //     break;
            case "gloveUpgradeMeleeDmg":
                gameConstants.meleeLevel1Damage *= 1.25f;
                break;

            // case "bootUpgradeDblJmp":
            //     break;
            // case "bootUpgradeSprint":
            //     break;
            case "bootUpgradeDecay":
                gameConstants.startingDecay *= 0.75f;
                break;

        }
            

    }
    
}
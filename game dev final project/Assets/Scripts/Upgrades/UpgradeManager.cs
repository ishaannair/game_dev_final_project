using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public GameConstants gameConstants;
    public FloatVariable playerRangedDamage;
    public void UpdateUpgrade(string upgradeName){
        bool upgradeCheck = (bool)gameConstants.GetType().GetField(upgradeName).GetValue(gameConstants);
        if (!upgradeCheck){
            gameConstants.GetType().GetField(upgradeName).SetValue(gameConstants,true);
            UpdateStats(upgradeName);
            Debug.Log((bool)gameConstants.GetType().GetField(upgradeName).GetValue(gameConstants));
        }
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
                gameConstants.playerSpeed += 5;
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName =  "GameConstants", menuName =  "ScriptableObjects/GameConstants", order =  1)]
public  class GameConstants : ScriptableObject
{
    // for Resources values
    public int totalScrap = 0;
    public int totalBlueprints = 0;

    // for HP Decay calculation
    public int startingPlayerHP = 100;
    public int startingTimer = 600;

    // for Player movement
    public int startingPlayerMaxSpeed = 5;
    public int startingPlayerJumpSpeed = 10;
    public int startingPlayerForce = 100;

    // for Consumables cost
    public int ductTapeScrapCost = 15;
    public int adrenalScrapCost = 25;
    public int radXScrapCost = 35;
    public int thermalPasteScrapCost = 50;

    // for Melee Upgrades cost
    public int meleeLevel2BlueprintsCost = 3;
    public int meleeLevel2ScrapCost = 50;
    public int meleeLevel3BlueprintsCost = 5;
    public int meleeLevel3ScrapCost = 100;

    // for Ranged Weapon Unlock cost
    public int shotgunBlueprintsCost = 3;
    public int shotgunScrapCost = 50;
    public int rocketBlueprintsCost = 7;
    public int rocketScrapCost = 125;

    // for Suit Upgrade cost
    public int level1UpgradeBlueprintsCost = 1;
    public int level1UpgradeScrapCost = 30;
    public int level2UpgradeBlueprintsCost = 2;
    public int level2UpgradeScrapCost = 65;
    public int level3UpgradeBlueprintsCost = 3;
    public int level3UpgradeScrapCost = 100;

    // for Weapon equipped

    public GunType gunType = GunType.blaster;

    // for Weapon Cooldowns
    public float blasterReloadTime = 15.0f;
    public float shotgunReloadTime = 20.0f;
    public float rocketReloadTime = 30.0f;
    public bool onCooldown = false;

    // for Weapon Damage
    public float blasterDamage = 10.0f;
    public float shotgunDamage = 15.0f;
    public float rocketDamage = 100.0f;
    public float rocketRadius = 2.25f;

    // for Weapon Capacity/Ammo Count
    public  int blasterAmmoClip = 12;
    public  int shotgunAmmoClip = 6;
    public  int rocketAmmoClip =  2;
    
    // for Melee Weapon 
    public  int meleeLevel = 1;
    public  float meleeLevel1Damage =  10.0f;
    public  float meleeLevel2Damage =  20.0f;
    public  float meleeLevel3Damage =  30.0f;

}
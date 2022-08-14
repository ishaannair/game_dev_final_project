using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName =  "GameConstants", menuName =  "ScriptableObjects/GameConstants", order =  1)]
public  class GameConstants : ScriptableObject
{
    // for Resources values
    public int totalScrap = 0;
    public int totalBlueprints = 0;
    public int scrapRate = 5;

    // for HP Decay calculation
    public float startingPlayerHP = 100;
    public float startingTimer = 600;
    public float startingDecay = 5;

    // for Player movement
    public float startingPlayerMaxSpeed = 10;
    public float startingPlayerJumpSpeed = 10;
    public float startingPlayerForce = 100;
    public float playerSpeed = 20;
    public bool playerFaceRightState = false;   

    // for Player Jump
    public float playerJumpDuration = 1.0f;

    // for Player Dash
    public float playerDashDuration = 0.25f;
    public float playerDashCooldownDuration = 0.1f;

    // for Player Slash
    public float playerSlashDuration = 0.2f;
    public float playerSlashCooldownDuration = 0.1f;

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
    public GunElement gunElement = GunElement.neutral;
    public float elementMultiplier = 1.5f;

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

    // for Weapon sprite position
    public Vector3 shotgunLeftSprite = new Vector3(-0.12f, -0.01f, 0.0f);
    public Vector3 shotgunRightSprite = new Vector3(0.09f, -0.01f, 0.0f);
    public Vector3 blasterLeftSprite = new Vector3(-0.033f, -0.058f, 0.0f);
    public Vector3 blasterRightSprite = new Vector3(0.053f, -0.058f, 0.0f);
    public Vector3 rocketLeftSprite = new Vector3(0.105f, -0.046f, 0.0f);
    public Vector3 rocketRightSprite = new Vector3(-0.078f, -0.046f, 0.0f);

    // for Weapon pool position
    public Vector3 shotgunLeftPool= new Vector3(0.403f, 0.035f, 0.0f);
    public Vector3 shotgunRightPool = new Vector3(-0.395f, 0.035f, 0.0f);
    public Vector3 blasterRightPool = new Vector3(-0.8f, 0.027f, 0.0f);
    public Vector3 blasterLeftPool = new Vector3(0.789f, 0.027f, 0.0f);
    public Vector3 rocketRightPool = new Vector3(-0.688f, 0.019f, 0.0f);
    public Vector3 rocketLeftPool = new Vector3(0.692f, 0.019f, 0.0f);
    
    // for Melee Weapon 
    public  int meleeLevel = 1;
    public  float meleeLevel1Damage =  10.0f;
    public  float meleeLevel2Damage =  20.0f;
    public  float meleeLevel3Damage =  30.0f;


    // for Enemy values
    public float jumperHealth = 20.0f;
    public float spitterHealth = 20.0f;
    public float exploderHealth = 20.0f;

    // For Upgrade Check
    public bool helmetUpgradeResource = false;
    public bool helmetUpgradeDamage = false;
    public bool helmetUpgradeReload = false;
    public bool torsoUpgradeHP = false;
    public bool torsoUpgradeDecay = false;
    public bool torsoUpgradeSpeed = false;
    public bool gloveUpgradeAtkSpd = false;
    public bool gloveUpgradeDblDmg = false;
    public bool gloveUpgradeMeleeDmg = false;
    public bool bootUpgradeDblJmp = false;
    public bool bootUpgradeDodge = false;
    public bool bootUpgradeDecay = false;
    // for Game Mechanics
    public float enemySightlines = 15f;
    public float enemyKnockbackDistance = 2f;
    //for Enemy Attack
    public float jumperDamage = 4f;
    public float spitterDamage = 2f;
    public float exploderDamage = 8f;
    public float bossMeleeDamage = 10f;
    public float bossFireballDamage = 5f;

    public float bossSlashDuration = 0.5f;
    
    public float bossMiniHealth = 100f;
    public float bossHealth = 200f;

    public float enemyKnockbackTime = 10;
}
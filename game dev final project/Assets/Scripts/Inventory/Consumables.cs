using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Consumables", menuName = "ScriptableObjects/Consumables", order = 4)]
public class Consumables : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif
	// index in the UI
    public ConsumablesIndex index;
	// sprite in the UI
    public Sprite consumablesSprite;
    
    // list of things any powerup can do
    public int healthBooster;
    public int damageSpeedBooster;
    public int slowDecayBooster;
    public int cooldownBooster;



	// effect of powerup
    public int duration;

    public List<int> Utilise(){
        return new List<int> {healthBooster, damageSpeedBooster, slowDecayBooster, cooldownBooster};
    }

    public void Reset(){
        healthBooster = 0;
        damageSpeedBooster = 0;
        slowDecayBooster = 0;
        cooldownBooster = 0;
    }

    public void Enhance(int healthBooster, int damageSpeedBooster, int slowDecayBooster, int cooldownBooster){
        healthBooster += healthBooster;
        damageSpeedBooster += damageSpeedBooster;
        slowDecayBooster += slowDecayBooster;
        cooldownBooster += cooldownBooster;
    }
}
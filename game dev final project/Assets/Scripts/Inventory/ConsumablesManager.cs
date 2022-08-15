using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ConsumablesIndex
{
    DUCTTAPE = 0,
    ADRENAL = 1,
    RADX = 2,
    THERMALPASTE = 3
}
public class ConsumablesManager : MonoBehaviour
{
    // reference of all player stats affected
    public FloatVariable playerHealth;
    public FloatVariable playerMeleeDamage;
    public FloatVariable playerRangedDamage;

    public FloatVariable playerMoveSpeed;
    public FloatVariable playerHealthDecay;
    public IntVariable playerCooldown;
    public FloatVariable cooldownMultiplier;


    public ConsumablesInventory consumablesInventory;
    public List<GameObject> consumablesIcons;
    public Sprite defaultSprite;

    void Start()
    {   
        cooldownMultiplier.SetValue(1.0f);
        if (!consumablesInventory.gameStarted)
        {
            consumablesInventory.gameStarted = true;
            consumablesInventory.Setup(consumablesIcons.Count);
            resetConsumables();
        }
        else
        {
            // re-render the contents of the powerup from the previous time
            for (int i = 0; i < consumablesInventory.Items.Count; i++)
            {
                Consumables c = consumablesInventory.Get(i);
                if (c != null)
                {
                    AddConsumablesUI(i, c.consumablesSprite);
                } 
                else{
                    consumablesIcons[i].SetActive(false);
                }
            }
        }
    }
        
    public void resetConsumables()
    {
        for (int i = 0; i < consumablesIcons.Count; i++)
        {
            // consumablesIcons[i].SetActive(false);
            consumablesIcons[i].GetComponent<Image>().sprite = defaultSprite;
        }
    }
        
    void AddConsumablesUI(int index, Sprite s)
    {
        consumablesIcons[index].GetComponent<Image>().sprite = s;
        consumablesIcons[index].SetActive(true);
    }

    public void AddConsumable(Consumables c, int index)
    {
        consumablesInventory.Add(c, (int)index);
        AddConsumablesUI((int)index, c.consumablesSprite);
    }

    void RemoveConsumablesUI(int index)
    {
        consumablesIcons[index].GetComponent<Image>().sprite = null;
        consumablesIcons[index].SetActive(false);
    }

    public void RemoveConsumable(Consumables c, int index)
    {   
        if (c.healthBooster>0){
            // Debug.Log("health");
            playerHealth.ApplyChange(c.healthBooster); // Increase current hp value
        } else if (c.damageSpeedBooster>0){
            // Debug.Log("damage");

            // Increase damage and move speed
            playerMeleeDamage.ApplyChange(c.damageSpeedBooster);
            playerRangedDamage.ApplyChange((float) c.damageSpeedBooster/10);
            playerMoveSpeed.ApplyChange(c.damageSpeedBooster/5);
            StartCoroutine(removeEffect("adrenal", c));
        } else if (c.cooldownBooster>0){
            Debug.Log("cooldown");
            cooldownMultiplier.SetValue(cooldownMultiplier.Value * (float)c.cooldownBooster/10); // Decrease cooldowns
            Debug.Log(cooldownMultiplier.Value);
            StartCoroutine(removeEffect("thermal", c));
        } else if (c.slowDecayBooster>0){
            // Debug.Log("decay");
            if (playerHealthDecay.Value - c.slowDecayBooster < 0){
                float originalHealthDecay = playerHealthDecay.Value;
                playerHealthDecay.ApplyChange(-playerHealthDecay.Value); // Stops negative health decay (causes player to heal over time)
                StartCoroutine(removeEffect("radX", c, originalHealthDecay));
            } else{
                playerHealthDecay.ApplyChange(-c.slowDecayBooster); // Decrease the decay
                StartCoroutine(removeEffect("radX", c));
            }
        }
        consumablesInventory.Remove((int)index);
        RemoveConsumablesUI((int)index);
    }

    IEnumerator removeEffect(string consumableType, Consumables c){
        // Debug.Log(c.duration);
        yield return new WaitForSeconds(c.duration);
        if (consumableType == "adrenal"){
            // Debug.Log("damage dropping");
            playerMeleeDamage.ApplyChange(-c.damageSpeedBooster);
            playerRangedDamage.ApplyChange((float) -c.damageSpeedBooster/10);
            playerMoveSpeed.ApplyChange(-c.damageSpeedBooster/5);
        } else if (consumableType == "thermal"){
            Debug.Log("cooldown dropping");
            // cooldownMultiplier.ApplyChange(-c.cooldownBooster);
            cooldownMultiplier.SetValue(cooldownMultiplier.Value * 2/3); // Decrease cooldowns
            Debug.Log(cooldownMultiplier.Value);
        } else if (consumableType == "radX"){
            // Debug.Log("decay dropping");
            playerHealthDecay.ApplyChange(c.slowDecayBooster);
        }
    }

    IEnumerator removeEffect(string consumableType, Consumables c, float value){
        yield return new WaitForSeconds(c.duration);
        if (consumableType == "radX"){
            // Debug.Log("decay dropping");
            playerHealthDecay.ApplyChange(value);
        }
    }

    public void OnApplicationQuit()
    {
        ResetValues();
    }

    public void ResetValues()
    {
        consumablesInventory.Clear();
        resetConsumables();
    }
    
}
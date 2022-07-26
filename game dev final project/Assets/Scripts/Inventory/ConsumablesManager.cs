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
    public IntVariable playerHealth;
    public IntVariable playerMeleeDamage;
    public IntVariable playerRangedDamage;

    public IntVariable playerMoveSpeed;
    public IntVariable playerHealthDecay;
    public IntVariable playerCooldown;

    public ConsumablesInventory consumablesInventory;
    public List<GameObject> consumablesIcons;
    public Sprite defaultSprite;

    void Start()
    {
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
        consumablesInventory.Remove((int)index);
        RemoveConsumablesUI((int)index);
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
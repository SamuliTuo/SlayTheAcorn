using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;
    
    public List<Potion> playerPotions = new List<Potion>();
    public int acornCount = 0;

    private Dictionary<StatusEffect, int> currentStatuses = new Dictionary<StatusEffect, int>();
    [SerializeField] private float playerHP = 50;
    public bool isHallusinating = false;
    bool hasShield = false;
    public bool stunned = false;

    public int battle = 0;


    void Awake()
    {
        if(PlayerInventory.instance) {
            Destroy(this.gameObject);
        }
        else{
            PlayerInventory.instance = this;
            Debug.Log("INSTANCE CREATED");
            DontDestroyOnLoad(this.gameObject);    
        }
    }

    public void AddAcorns(int count) { acornCount += count; }
    public bool TryToRemoveAcorns(int amount)
    {
        if (acornCount >= amount)
        {
            acornCount -= amount;
            return true;
        }
        return false;
    }

    public void AddPotion(Potion pot)
    {
        playerPotions.Add(pot);
    }

    public Potion GetRandomPotion()
    {
        if (playerPotions.Count > 0)
        {
            int i = Random.Range(0, playerPotions.Count);
            Potion pot = playerPotions[i];
            playerPotions.RemoveAt(i);
            return pot;
        }
        else return null;
    }

    // Getting damaged
    public bool ApplyTurnEffects()
    {
        if (currentStatuses.Count > 0)
        {
            Dictionary<StatusEffect, int> updatedEffects = new Dictionary<StatusEffect, int>();
            foreach (KeyValuePair<StatusEffect, int> status in currentStatuses)
            {
                if (ApplyEffect(status.Key, status.Value) == true)
                {
                    print("player died! oh no!");
                    return true;
                }

                if (status.Value > 1)
                    updatedEffects.Add(status.Key, status.Value - 1);
                else
                {
                    if (status.Key == StatusEffect.HALLUSINATION)
                        isHallusinating = false;
                    if (status.Key == StatusEffect.SHIELD)
                        hasShield = false;
                }
            }
            currentStatuses.Clear();

            if (updatedEffects.Count > 0)
            {
                currentStatuses = updatedEffects;
            }
        }
        return false;
    }

    bool ApplyEffect(StatusEffect eff, int turnsLeft)
    {
        if (eff == StatusEffect.PARALYSIS)
        {
            stunned = true;
        }
        else if (eff == StatusEffect.HALLUSINATION)
        {
            isHallusinating = true;
            print("I'm tripping off my balls here maaan");
        }
        else if (eff == StatusEffect.SHIELD)
        {
            hasShield = true;
            print("Got a shield nice! btw I'm the player");
        }
        else if (eff == StatusEffect.DOT)
        {
            print("taking a dot but idk how much lmao");
            if (AddHpAndCheckIfDead(turnsLeft))
            {
                return true;
            }
        }
        return false;
    }

    public void AddStatusEffect(StatusEffect effect, int turns)
    {
        currentStatuses.Add(effect, turns);
    }

    public bool AddHpAndCheckIfDead(float amount)
    {
        if (!hasShield)
        {
            playerHP += amount;
        }
        
        print("player took damage! hp left: " + playerHP);
        if (playerHP <= 0)
        {
            playerHP = 0;
            return true;
        }
        return false;
    }











    //   S C E N E    M A N A G E R   \\

    public void moveToMainScreen()
    {
        SceneManager.LoadScene(0);
    }
    public void moveToForest()
    {
        SceneManager.LoadScene(1);
    }
    public void moveToBattle()
    {
        SceneManager.LoadScene(2);
    }
    public void moveToAlchemy()
    {
        SceneManager.LoadScene(3);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    
    public static PlayerInventory instance;
    
    public List<Potion> playerPotions = new List<Potion>();
    public int acornCount = 0;

    private Dictionary<StatusEffect, int> currentStatuses = new Dictionary<StatusEffect, int>();
    [SerializeField] private float playerHP = 50;

    
    void Start()
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
                {
                    updatedEffects.Add(status.Key, status.Value - 1);
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
        if (eff == StatusEffect.CONFUSION)
        {
            print("I am confusion");
        }
        else if (eff == StatusEffect.HALLUSINATION)
        {
            print("I'm tripping off my balls here maaan");
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
        playerHP += amount;
        if (playerHP <= 0)
        {
            playerHP = 0;
            return true;
        }
        return false;
    }
}

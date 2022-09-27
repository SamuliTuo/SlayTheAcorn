using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    
    public static PlayerInventory instance;
    
    public List<Potion> playerPotions = new List<Potion>();
    public int acornCount = 0;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffect { NONE, CONFUSION, HALLUSINATION }
public class EnemyController : MonoBehaviour
{
    Dictionary<StatusEffect, int> currentStatuses = new Dictionary<StatusEffect, int>();
    float hp;

    public bool ReceivePotionAttackAndCheckIfDead(Potion potion)
    {
        print("AAAPUUUVAAAA");
        if (potion.statusEff != StatusEffect.NONE)
        {
            currentStatuses.Add(potion.statusEff, potion.effectDurationInTurns);
        }
        
        hp -= potion.damage;
        if (hp < 0)
            return true;
        else
            return false;
    }

    public void ApplyTurnEffects()
    {
        if (currentStatuses.Count > 0)
        {
            foreach (KeyValuePair<StatusEffect, int> status in currentStatuses)
            {
                //status.Value
            }
        }
    }
}

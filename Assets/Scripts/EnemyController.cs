using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffect { NONE, DOT, CONFUSION, HALLUSINATION }
public class EnemyController : MonoBehaviour
{
    Dictionary<StatusEffect, int> currentStatuses = new Dictionary<StatusEffect, int>();
    float hp = 100;

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

    public void ChooseAttack()
    {

    }

    public void ApplyTurnEffects()
    {
        if (currentStatuses.Count > 0)
        {
            Dictionary<StatusEffect, int> updatedEffects = new Dictionary<StatusEffect, int>();
            foreach (KeyValuePair<StatusEffect, int> status in currentStatuses)
            {
                //apply effect
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
    }
}

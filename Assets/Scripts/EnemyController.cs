using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffect { NONE, DOT, CONFUSION, HALLUSINATION }
public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyScriptable enemy;
    private Dictionary<StatusEffect, int> currentStatuses = new Dictionary<StatusEffect, int>();
    private float hp = 100;

    private void Start()
    {
        print(enemy);
    }
    public bool ReceivePotionAttackAndCheckIfDead(Potion potion)
    {
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
        float randomizedAttack = enemy.normalAtt_chance + enemy.status1_chance + enemy.status2_chance + enemy.status3_chance + enemy.dot_chance
            * Random.Range(0.00f, 1.00f);
        if (randomizedAttack > enemy.normalAtt_chance)
        {
            print("normal attack");
        }
        else if (randomizedAttack > enemy.normalAtt_chance + enemy.status1_chance)
        {
            print("status1");
        }
        else if (randomizedAttack > enemy.normalAtt_chance + enemy.status1_chance + enemy.status2_chance)
        {
            print("status2");
        }
        else if (randomizedAttack > enemy.normalAtt_chance + enemy.status1_chance + enemy.status2_chance + enemy.status3_chance)
        {
            print("status3");
        }
        else
        {
            print("status3");
        }



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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffect { NONE, DOT, PARALYSIS, HALLUSINATION, SHIELD }
public class EnemyController : MonoBehaviour
{
    private GameManager game;
    private EnemyScriptable enemy;
    private Dictionary<StatusEffect, int> currentStatuses = new Dictionary<StatusEffect, int>();
    private float hp;
    bool isHallusinating = false;
    bool hasShield = false;
    public bool stunned = false;


    public void ResetEnemy() { enemy = null; }
    public void SetEnemy(EnemyScriptable _enemy, GameManager _game)
    {
        enemy = _enemy;
        game = _game;
        hp = _enemy.enemyHP;
    }
    
    public bool ReceivePotionAttackAndCheckIfDead(Potion potion, bool halluMultiplier)
    {
        if (potion.statusEff != StatusEffect.NONE)
        {
            currentStatuses.Add(potion.statusEff, potion.effectDurationInTurns);
        }
        float dmg = Random.Range(potion.damage.x, potion.damage.y);
        dmg = halluMultiplier ? dmg * 0.5f : dmg;
        if (AddHpAndCheckIfDead(dmg))
        {
            return true;
        }
        return false;
    }
    bool AddHpAndCheckIfDead(float amount)
    {
        if (!hasShield)
        {
            hp -= amount;
        }
        print("Enemy hp left: " + hp);
        if (hp <= 0)
        {
            hp = 0;
            return true;
        }
        else
            return false;
    }

    public void ChooseAttack()
    {
        float rand = 
            enemy.normalAtt_chance + enemy.status1_chance + enemy.status2_chance + enemy.status3_chance + enemy.dot_chance
            * Random.Range(0.00f, 1.00f);

        if (rand > enemy.normalAtt_chance)
        {
            float dmg = -Random.Range(enemy.normalAttackDamageRange.x, enemy.normalAttackDamageRange.y);
            dmg = isHallusinating ? dmg * 0.5f : dmg;
            if (PlayerInventory.instance.AddHpAndCheckIfDead(dmg))
                game.PlayerAlive = false;
        }
        else if (rand > enemy.normalAtt_chance + enemy.status1_chance)
            PlayerInventory.instance.AddStatusEffect(enemy.statusEffect1, enemy.status1_durationInTurns);
        else if (rand > enemy.normalAtt_chance + enemy.status1_chance + enemy.status2_chance)
            PlayerInventory.instance.AddStatusEffect(enemy.statusEffect2, enemy.status2_durationInTurns);
        else if (rand > enemy.normalAtt_chance + enemy.status1_chance + enemy.status2_chance + enemy.status3_chance)
            PlayerInventory.instance.AddStatusEffect(enemy.statusEffect1, enemy.status1_durationInTurns);
        else
            PlayerInventory.instance.AddStatusEffect(enemy.dot, enemy.dot_durationInTurns);
    }

    public bool ApplyTurnEffects()
    {
        if (currentStatuses.Count > 0)
        {
            Dictionary<StatusEffect, int> updatedEffects = new Dictionary<StatusEffect, int>();
            foreach (KeyValuePair<StatusEffect, int> status in currentStatuses)
            {
                if (ApplyEffect(status.Key, status.Value))
                {
                    currentStatuses.Clear();
                    return true;
                }

                if (status.Value > 1)
                {
                    updatedEffects.Add(status.Key, status.Value - 1);
                }
                else
                {
                    if (status.Key == StatusEffect.HALLUSINATION)
                    {
                        isHallusinating = false;
                    }
                    if (status.Key == StatusEffect.SHIELD)
                    {
                        hasShield = false;
                    }
                    if (status.Key == StatusEffect.PARALYSIS)
                    {
                        stunned = false;
                    }
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
        }
        else if (eff == StatusEffect.SHIELD)
        {
            hasShield = true;
        }
        else if (eff == StatusEffect.DOT)
        {
            print("taking a dot oh no!");
            if (AddHpAndCheckIfDead(turnsLeft))
            {
                return true;
            }
        }
        return false;
    }
}

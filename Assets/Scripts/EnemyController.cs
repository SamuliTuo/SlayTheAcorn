using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffect { NONE, DOT, CONFUSION, HALLUSINATION }
public class EnemyController : MonoBehaviour
{
    private GameManager game;
    private EnemyScriptable enemy;
    private Dictionary<StatusEffect, int> currentStatuses = new Dictionary<StatusEffect, int>();
    private float hp;


    public void ResetEnemy() { enemy = null; }
    public void SetEnemy(EnemyScriptable _enemy, GameManager _game)
    {
        enemy = _enemy;
        game = _game;
        hp = _enemy.enemyHP;
    }
    
    public bool ReceivePotionAttackAndCheckIfDead(Potion potion)
    {
        if (potion.statusEff != StatusEffect.NONE)
        {
            currentStatuses.Add(potion.statusEff, potion.effectDurationInTurns);
        }
        if (AddHpAndCheckIfDead(Random.Range(potion.damage.x, potion.damage.y)))
        {
            return true;
        }
        return false;
    }
    bool AddHpAndCheckIfDead(float amount)
    {
        hp -= amount;
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
        float randomizedAttack = enemy.normalAtt_chance + enemy.status1_chance + enemy.status2_chance + enemy.status3_chance + enemy.dot_chance
            * Random.Range(0.00f, 1.00f);
        if (randomizedAttack > enemy.normalAtt_chance)
        {
            if (PlayerInventory.instance.AddHpAndCheckIfDead(Random.Range(enemy.normalAttackDamageRange.x, enemy.normalAttackDamageRange.y)))
                game.PlayerAlive = true;
        }
        else if (randomizedAttack > enemy.normalAtt_chance + enemy.status1_chance)
            PlayerInventory.instance.AddStatusEffect(enemy.statusEffect1, enemy.status1_durationInTurns);
        else if (randomizedAttack > enemy.normalAtt_chance + enemy.status1_chance + enemy.status2_chance)
            PlayerInventory.instance.AddStatusEffect(enemy.statusEffect2, enemy.status2_durationInTurns);
        else if (randomizedAttack > enemy.normalAtt_chance + enemy.status1_chance + enemy.status2_chance + enemy.status3_chance)
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
}

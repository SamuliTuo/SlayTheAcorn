using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/NewEnemy", order = 1)]
public class EnemyScriptable : ScriptableObject
{
    [Header("Chance means how often will the enemy use: 0 = never, 1 = always")]

    public Vector2 normalAttackDamageRange = new Vector2(5.0f, 10.0f);
    public float normalAtt_chance = 0.5f;
    public StatusEffect statusEffect1 = StatusEffect.NONE;
    public float status1_chance = 0.1f;
    public StatusEffect statusEffect2 = StatusEffect.NONE;
    public float status2_chance = 0.1f;
    public StatusEffect statusEffect3 = StatusEffect.NONE;
    public float status3_chance = 0.1f;
    [Space(10)]
    [Header("DONT CHANGE THIS. lololol! Keep it as 'DOT'")]
    public StatusEffect dot = StatusEffect.DOT;
    [Header("Change these tho! :)  Just keep the above one as is. Thank you!")]
    public float dot_damagePerTick = 0.5f;
    public float dot_chance = 0.2f;
}

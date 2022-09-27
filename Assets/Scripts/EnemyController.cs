using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffect { NONE, CONFUSION, HALLUSINATION }
public class EnemyController : MonoBehaviour
{
    
    float hp;

    public bool ReceivePotion(
        float healthAdd, 
        StatusEffect statusEffect = StatusEffect.NONE, 
        int statusEffectTurnDuration = 1
        )
    {
        hp += healthAdd;
        if (hp < 0)
            return true;
        else
            return false;
    }
}

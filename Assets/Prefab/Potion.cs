using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPotion", menuName = "ScriptableObjects/Potions", order = 1)]
public class Potion : ScriptableObject
{
    public Sprite image;
    public float damage = 1;
    public StatusEffect statusEff = StatusEffect.NONE;
    public int effectDurationInTurns = 1;
}

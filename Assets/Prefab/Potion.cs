using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPotion", menuName = "ScriptableObjects/Potions", order = 1)]
public class Potion : ScriptableObject
{
    public Sprite image;
    public Vector2 damage = new Vector2(4, 6);
    public StatusEffect statusEff = StatusEffect.NONE;
    public int effectDurationInTurns = 1;
}

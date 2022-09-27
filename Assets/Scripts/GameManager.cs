using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    GameObject canvas;
    EnemyController currentEnemy;

    [SerializeField] private Potion potion_01 = null;
    
    
    void Start()
    {
        canvas = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void BattleStart(ene)
    //{

    //}

    public void ThrowPotion()
    {
        if (currentEnemy == null)
        {
            currentEnemy = canvas.transform.Find("Enemy").GetComponent<EnemyController>();
        }
        currentEnemy.ReceivePotion(potion_01.damage, potion_01.statusEff, potion_01.effectDurationInTurns);
    }
}

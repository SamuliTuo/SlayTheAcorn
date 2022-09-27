using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Turn { PLAYERTURN, ENEMYTURN }
public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerInventory plrInventory = null;
    GameObject canvas;
    GameObject currentEnemy;
    bool playerAlive;
    bool enemyAlive;
    Coroutine batteRoutine = null;

    Turn currentTurn = Turn.PLAYERTURN;

    Potion potionSlot_01;
    Potion potionSlot_02;
    Potion potionSlot_03;
    Potion chosenPotion = null;

    private GameObject enemy_01;
    private GameObject enemy_02;
    private GameObject enemy_03;
    private GameObject enemy_04;
    private GameObject enemy_05;
    private GameObject enemy_06;
    private GameObject enemy_07;
    private GameObject enemy_08;
    private GameObject enemy_09;
    private GameObject enemy_10;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        enemy_01 = Resources.Load("Enemy_01") as GameObject;
        if (batteRoutine == null)
        {
            StartCoroutine(BattlePhase());
        }
    }

    IEnumerator BattlePhase()
    {
        BattleStart(enemy_01);
        yield return null;
        while (playerAlive && enemyAlive)
        {
            if (currentTurn == Turn.PLAYERTURN)
            {
                if (chosenPotion == null)
                {
                    yield return null;
                }

                currentEnemy.GetComponent<EnemyController>().ReceivePotionAttackAndCheckIfDead(chosenPotion);
                yield return new WaitForSeconds(2);
            }

            else if (currentTurn == Turn.ENEMYTURN)
            {
                currentEnemy.GetComponent<EnemyController>().ApplyTurnEffects();
                yield return new WaitForSeconds(2);
            }
        }
        print("Combat ended, player or enemy died");
    }
    


    void BattleStart(GameObject enemy)
    {
        chosenPotion = null;
        currentTurn = Turn.PLAYERTURN;
        currentEnemy = enemy;
        playerAlive = true;
        enemyAlive = true;
    }
    
    void ChangeTurns()
    {
        if (currentTurn == Turn.PLAYERTURN)
        {
            PlayerTurn();
            currentTurn = Turn.ENEMYTURN;
        }
        else if (currentTurn == Turn.ENEMYTURN)
        {
            EnemyTurn();
            currentTurn = Turn.PLAYERTURN;
        }
    }
    void EnemyTurn()
    {

    }
    void PlayerTurn()
    {
        potionSlot_01 = plrInventory.GetRandomPotion();
        potionSlot_02 = plrInventory.GetRandomPotion();
        potionSlot_03 = plrInventory.GetRandomPotion();
    }



    public void ThrowPotion_slot1()
    {
        chosenPotion = potionSlot_01;
        plrInventory.AddPotion(potionSlot_02);
        plrInventory.AddPotion(potionSlot_03);
    }
    public void ThrowPotion_slot2()
    {
        chosenPotion = potionSlot_02;
        plrInventory.AddPotion(potionSlot_01);
        plrInventory.AddPotion(potionSlot_03);
    }
    public void ThrowPotion_slot3()
    {
        chosenPotion = potionSlot_03;
        plrInventory.AddPotion(potionSlot_01);
        plrInventory.AddPotion(potionSlot_02);
    }
}

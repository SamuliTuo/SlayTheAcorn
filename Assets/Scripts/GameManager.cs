using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Turn { PLAYERTURN, ENEMYTURN }
public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerInventory plrInventory = null;
    [SerializeField] private Sprite emptyBottleImage = null;

    GameObject canvas;
    GameObject currentEnemy;
    bool playerAlive;
    bool enemyAlive;
    Coroutine battleCoroutine = null;

    Turn currentTurn = Turn.PLAYERTURN;

    Image canvas_potionSlot01;
    Image canvas_potionSlot02;
    Image canvas_potionSlot03;
    Image canvas_rerollButton;
    Image canvas_acornButton;
    
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
        canvas_potionSlot01 = canvas.transform.Find("potionSlot_01").GetComponent<Image>();
        canvas_potionSlot02 = canvas.transform.Find("potionSlot_02").GetComponent<Image>();
        canvas_potionSlot03 = canvas.transform.Find("potionSlot_03").GetComponent<Image>();
        canvas_rerollButton = canvas.transform.Find("potions_reroll").GetComponent<Image>();
        canvas_acornButton = canvas.transform.Find("acornShoot").GetComponent<Image>();
        if (battleCoroutine == null)
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
                else
                {
                    currentEnemy.GetComponent<EnemyController>().ReceivePotionAttackAndCheckIfDead(chosenPotion);
                    chosenPotion = potionSlot_01 = potionSlot_02 = potionSlot_03 = null;
                    TogglePlayerButtons(false);
                    yield return new WaitForSeconds(1);
                    EnemyTurn();
                    print("enemy turn");
                    yield return null;
                }

            }
            else if (currentTurn == Turn.ENEMYTURN)
            {
                currentEnemy.GetComponent<EnemyController>().ApplyTurnEffects();
                yield return new WaitForSeconds(1);
                currentEnemy.GetComponent<EnemyController>().ChooseAttack();
                yield return new WaitForSeconds(0.5f);
                PlayerTurn();
                print("player turn");
                yield return null;
            }
        }
        print("Combat ended, player or enemy died");
    }
    


    void BattleStart(GameObject enemy)
    {
        chosenPotion = null;
        currentEnemy = enemy;
        playerAlive = true;
        enemyAlive = true;
        PlayerTurn();
    }

    void EnemyTurn()
    {
        currentTurn = Turn.ENEMYTURN;
    }
    void PlayerTurn()
    {
        currentTurn = Turn.PLAYERTURN;
        TogglePlayerButtons(true);
        print("toggled buttons on");
        FillPotionSlots();
    }

    public void ThrowPotion_slot1()
    {
        chosenPotion = potionSlot_01;
        if (potionSlot_02 != null)
        {
            plrInventory.AddPotion(potionSlot_02);
        }
        if (potionSlot_03 != null)
        {
            plrInventory.AddPotion(potionSlot_03);
        }
    }
    public void ThrowPotion_slot2()
    {
        chosenPotion = potionSlot_02;
        if (potionSlot_01 != null)
        {
            plrInventory.AddPotion(potionSlot_01);
        }
        if (potionSlot_03 != null)
        {
            plrInventory.AddPotion(potionSlot_03);
        }
    }
    public void ThrowPotion_slot3()
    {
        chosenPotion = potionSlot_03;
        if (potionSlot_01 != null)
        {
            plrInventory.AddPotion(potionSlot_01);
        }
        if (potionSlot_02 != null)
        {
            plrInventory.AddPotion(potionSlot_02);
        }
    }
    public void RerollPotions()
    {
        if (potionSlot_01 != null)
        {
            plrInventory.AddPotion(potionSlot_01);
        }
        if (potionSlot_02 != null)
        {
            plrInventory.AddPotion(potionSlot_02);
        }
        if (potionSlot_03 != null)
        {
            plrInventory.AddPotion(potionSlot_03);
        }
        potionSlot_01 = potionSlot_02 = potionSlot_03 = null;
    }

    void TogglePlayerButtons(bool state)
    {
        canvas_potionSlot01.gameObject.SetActive(state);
        canvas_potionSlot02.gameObject.SetActive(state);
        canvas_potionSlot03.gameObject.SetActive(state);
        canvas_rerollButton.gameObject.SetActive(state);
        canvas_acornButton.gameObject.SetActive(state);
    }
    void FillPotionSlots()
    {
        // Potion 1
        potionSlot_01 = plrInventory.GetRandomPotion();
        if (potionSlot_01 != null)
        {
            canvas_potionSlot01.sprite = potionSlot_01.image;
        }
        else
        {
            canvas_potionSlot01.sprite = emptyBottleImage;
        }

        // Potion 2
        potionSlot_02 = plrInventory.GetRandomPotion();
        if (potionSlot_02 != null)
        {
            canvas_potionSlot02.sprite = potionSlot_02.image;
        }
        else
        {
            canvas_potionSlot02.sprite = emptyBottleImage;
        }

        // Potion 3
        potionSlot_03 = plrInventory.GetRandomPotion();
        if (potionSlot_03 != null)
        {
            canvas_potionSlot03.sprite = potionSlot_03.image;
        }
        else
        {
            canvas_potionSlot03.sprite = emptyBottleImage;
        }
    }
}

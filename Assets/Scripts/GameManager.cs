using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Turn { PLAYERTURN, ENEMYTURN }
public class GameManager : MonoBehaviour
{
    private PlayerInventory plrInventory;
    [SerializeField] private Sprite emptyBottleImage = null;
    [SerializeField] private int rerollPotionsAcornCost = 5;

    GameObject canvas;
    public bool PlayerAlive { get; set; }
    bool enemyAlive;
    Coroutine battleCoroutine = null;

    Turn currentTurn = Turn.PLAYERTURN;

    Image canvas_potionSlot01;
    Image canvas_potionSlot02;
    Image canvas_potionSlot03;
    Image canvas_rerollButton;
    Image canvas_acornButton;
    Image canvas_enemy;
    Image canvas_player;
    
    Potion potionSlot_01;
    Potion potionSlot_02;
    Potion potionSlot_03;
    Potion chosenPotion = null;

    private GameObject playerObject;
    private GameObject enemyObject;

    [SerializeField] private EnemyScriptable enemy_01;
    [SerializeField] private EnemyScriptable enemy_02;
    [SerializeField] private EnemyScriptable enemy_03;
    [SerializeField] private EnemyScriptable enemy_04;
    [SerializeField] private EnemyScriptable enemy_05;
    [SerializeField] private EnemyScriptable enemy_06;
    [SerializeField] private EnemyScriptable enemy_07;
    [SerializeField] private EnemyScriptable enemy_08;
    [SerializeField] private EnemyScriptable enemy_09;
    [SerializeField] private EnemyScriptable enemy_10;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        canvas_potionSlot01 = canvas.transform.Find("potionSlot_01").GetComponent<Image>();
        canvas_potionSlot02 = canvas.transform.Find("potionSlot_02").GetComponent<Image>();
        canvas_potionSlot03 = canvas.transform.Find("potionSlot_03").GetComponent<Image>();
        canvas_rerollButton = canvas.transform.Find("potions_reroll").GetComponent<Image>();
        canvas_acornButton = canvas.transform.Find("acornShoot").GetComponent<Image>();
        playerObject = canvas.transform.Find("Player").gameObject;
        canvas_player = playerObject.GetComponent<Image>();
        enemyObject = canvas.transform.Find("Enemy").gameObject;
        print(enemyObject.name);
        canvas_enemy = enemyObject.GetComponent<Image>();
        plrInventory = PlayerInventory.instance;
        if (battleCoroutine == null)
        {
            StartCoroutine(BattlePhase());
        }
    }

    IEnumerator BattlePhase()
    {
        BattleStart(enemy_01);

        yield return null;
        while (PlayerAlive && enemyAlive)
        {
            if (currentTurn == Turn.PLAYERTURN)
            {
                if (PlayerInventory.instance.ApplyTurnEffects() == true)
                    PlayerAlive = false;

                while (chosenPotion == null)
                    yield return null;


                if (enemyObject.GetComponent<EnemyController>().ReceivePotionAttackAndCheckIfDead(chosenPotion, plrInventory.isHallusinating) == true)
                {
                    enemyObject.GetComponent<EnemyController>().ResetEnemy();
                    enemyAlive = false;
                }
                chosenPotion = potionSlot_01 = potionSlot_02 = potionSlot_03 = null;
                TogglePlayerButtons(false);
                yield return new WaitForSeconds(1);
                EnemyTurnStart();
                print("enemy turn");
                yield return null;
            }

            else if (currentTurn == Turn.ENEMYTURN)
            {
                if (enemyObject.GetComponent<EnemyController>().ApplyTurnEffects() == true)
                {
                    enemyAlive = false;
                    yield return new WaitForSeconds(1);
                }
                else
                {
                    yield return new WaitForSeconds(1);
                    enemyObject.GetComponent<EnemyController>().ChooseAttack();
                    yield return new WaitForSeconds(0.5f);
                    if (PlayerAlive)
                    {
                        PlayerTurnStart();
                        print("player turn");
                    }
                    yield return null;
                }

            }
        }
        BattleEnd();
    }
    

    void BattleStart(EnemyScriptable enemy)
    {
        chosenPotion = null;
        enemyObject.GetComponent<EnemyController>().SetEnemy(enemy, this);
        canvas_enemy.sprite = enemy.enemySprite;
        PlayerAlive = true;
        enemyAlive = true;
        PlayerTurnStart();
    }
    void BattleEnd()
    {
        if (!PlayerAlive)
        {
            playerObject.SetActive(false);
        }
        if (!enemyAlive)
        {
            enemyObject.SetActive(false);
        }
    }

    void EnemyTurnStart()
    {
        currentTurn = Turn.ENEMYTURN;
    }
    void PlayerTurnStart()
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
        if (plrInventory.acornCount < rerollPotionsAcornCost)
            return;

        plrInventory.AddAcorns(-rerollPotionsAcornCost);

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

        FillPotionSlots();
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

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
    [SerializeField] private GameObject flying_potion_slot = null;
    [SerializeField] private GameObject endOfBattleScreen = null;
    [SerializeField] private GameObject playerDiedBattleEnd = null;
    [SerializeField] private Transform fightingText = null;

    [SerializeField] private Image hpBarEnemy = null;
    [SerializeField] private Image hpBarPlayer = null;

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

    [SerializeField] Potion acorn = null;
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
        BattleStart();

        tFightText = 0;
        fightingText.gameObject.SetActive(true);
        while (LerpingFightText() == true)
        {
            yield return null;
        }
        while (PlayerAlive && enemyAlive)
        {
            if (currentTurn == Turn.PLAYERTURN)
            {
                if (PlayerInventory.instance.ApplyTurnEffects() == true)
                    PlayerAlive = false;

                if (plrInventory.stunned == false)
                {
                    while (chosenPotion == null)
                        yield return null;
                    FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/Character/PlayerSling", gameObject);


                    flying_potion_slot.GetComponent<Image>().sprite = chosenPotion.image;
                    StartCoroutine(ThrowItemToEnemy());
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
            }

            else if (currentTurn == Turn.ENEMYTURN)
            {
                if (enemyObject.GetComponent<EnemyController>().ApplyTurnEffects() == true)
                {
                    enemyAlive = false;
                    yield return new WaitForSeconds(1);
                }
                else if (enemyObject.GetComponent<EnemyController>().stunned == false)
                {
                    FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/Character/EnemyAttack", gameObject);
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

    public void UpdateHPBar_enemy(float perc)
    {
        hpBarEnemy.fillAmount = perc;
    }
    public void UpdateHPBar_Player(float perc)
    {
        hpBarPlayer.fillAmount = perc;
    }

    float tFightText = 0;
    bool LerpingFightText()
    {
        if (tFightText < 1)
        {
            tFightText += Time.deltaTime;
            return true;
        }
        fightingText.gameObject.SetActive(false);
        return false;
    }

    Vector3 startPos = new Vector3(-201, -371, 0);
    Vector3 endPos = new Vector3(236, 554, 0);
    float t, perc;
    float lerpSpeed = 2;
    IEnumerator ThrowItemToEnemy()
    {
        flying_potion_slot.transform.position = startPos;
        flying_potion_slot.SetActive(true);
        t = perc = 0;
        while (t < 1)
        {
            flying_potion_slot.transform.localPosition = Vector3.Lerp(startPos, endPos, perc);
            t += Time.deltaTime * lerpSpeed;
            perc = Mathf.Sin((t * Mathf.PI) * 0.5f);
            yield return null;
        }
        flying_potion_slot.SetActive(false);
    }
    void BattleStart()
    {
        EnemyScriptable enemy;
        switch (plrInventory.battle)
        {
            case 0:
                enemy = enemy_01;
                break;
            case 1:
                enemy = enemy_02;
                break;
            case 2:
                enemy = enemy_03;
                break;
            case 3:
                enemy = enemy_04;
                break;
            default:
                enemy = enemy_01;
                break;
        }

        chosenPotion = null;
        enemyObject.GetComponent<EnemyController>().SetEnemy(enemy, this);
        canvas_enemy.sprite = enemy.enemySprite;
        PlayerAlive = true;
        enemyAlive = true;
        PlayerTurnStart();
    }
    void BattleEnd()
    {
        TogglePlayerButtons(false);
        if (!PlayerAlive)
        {
            plrInventory.battle = 0;
            playerObject.SetActive(false);
            playerDiedBattleEnd.SetActive(true);
        }
        else if (!enemyAlive)
        {
            enemyObject.SetActive(false);
            endOfBattleScreen.SetActive(true);
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

    public void ThrowAcorn()
    {
        if (plrInventory.acornCount > 0)
        {
            chosenPotion = acorn;
        }
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

    public void ContinueAdventure()
    {
        plrInventory.battle++;
        plrInventory.moveToForest();
    }
    public void GoBackHomeToBrewPotions()
    {
        plrInventory.battle = 0;
        plrInventory.moveToAlchemy();
    }
    public void GoToMainScreen()
    {
        plrInventory.battle = 0;
        plrInventory.moveToMainScreen();
    }
}

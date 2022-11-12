using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UpgradePanel : MonoBehaviour
{
    [Serializable]
     public struct Card
     {
         public Image back;
         public TextMeshProUGUI EffectText1;
         public TextMeshProUGUI EffectText2;
         public List<GameObject> colors;

         public bool isWeapon;
         [HideInInspector]public WeaponScriptable weapon;
         [HideInInspector]public StatScriptable stats;
         [HideInInspector]public bool selected;
     }

     public static UpgradePanel Instance;
     
     
    public StatScriptable[] commonCards;
    public StatScriptable[] uncommonCards;
    public StatScriptable[] rareCards;
    public StatScriptable[] epicCards;
    public WeaponScriptable[] epicWeapon;
    public StatScriptable[] legendaryCards;
    public WeaponScriptable[] legendaryWeapon;

    public int[] playerPos = new int[4];
    [Space]
    public GameObject[] startColors;
    private int cardTake;
    
    [Header("=== UI ===")]
    [SerializeField] Color colorCommon;
    [SerializeField] Color colorUncommon;
    [SerializeField] Color colorRare;
    [SerializeField] Color colorEpic;
    [SerializeField] Color colorlegendary;
    [Space]
    [SerializeField] Color takenColor;
    [Space]

    public Card[] cards;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        DrawCards();
        cardTake = 0;
        for (int i = 0; i < playerPos.Length; i++)
        {
            playerPos[i] = 0;
            startColors[i].SetActive(true);
            cards[i].selected = false;
        }

        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))PlayerMove(0,false);
        if(Input.GetKeyDown(KeyCode.E))PlayerMove(0,true);
        
        if(Input.GetKeyDown(KeyCode.A))PlayerMove(1,false);
        if(Input.GetKeyDown(KeyCode.D))PlayerMove(1,true);
        
        if(Input.GetKeyDown(KeyCode.I))PlayerMove(2,false);
        if(Input.GetKeyDown(KeyCode.P))PlayerMove(2,true);
        
        if(Input.GetKeyDown(KeyCode.K))PlayerMove(3,false);
        if(Input.GetKeyDown(KeyCode.Semicolon))PlayerMove(3,true);
        
        if(Input.GetKeyDown(KeyCode.W))SelectCard(0);
        if(Input.GetKeyDown(KeyCode.S))SelectCard(1);
        if(Input.GetKeyDown(KeyCode.O))SelectCard(2);
        if(Input.GetKeyDown(KeyCode.L))SelectCard(3);
    }


    private void DrawCards()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            StatScriptable cardStat = null;
            WeaponScriptable weaponStat = null;
            int dropRarity = Random.Range(0, 100);
            if (dropRarity < 45)
            {
                cardStat = commonCards[Random.Range(0, commonCards.Length)];
                cards[i].back.color = colorCommon;
            }
            else if (dropRarity < 70)
            {
                cardStat = uncommonCards[Random.Range(0, uncommonCards.Length)];
                cards[i].back.color = colorUncommon;
            }
            else if (dropRarity < 85)
            {
                cardStat = rareCards[Random.Range(0, rareCards.Length)];
                cards[i].back.color = colorRare;
            }
            else if (dropRarity < 95)
            {
                if(Random.value<.5) cardStat = epicCards[Random.Range(0, epicCards.Length)];
                else
                {
                    weaponStat = epicWeapon[Random.Range(0, epicCards.Length)];
                    cards[i].isWeapon = true;
                }
                cards[i].back.color = colorEpic;
            }
            else
            {
                if(Random.value<0.5f)cardStat = legendaryCards[Random.Range(0, legendaryCards.Length)];
                else
                {
                    weaponStat = legendaryWeapon[Random.Range(0, epicCards.Length)];
                    cards[i].isWeapon = true;
                }
                cards[i].back.color = colorlegendary;
            }

            cards[i].stats = cardStat;
            cards[i].weapon = weaponStat;
            
            string txt = ". . .";
            for (int j = 0; j < 2; j++)
            {
                //Debug.Log($"j:{j}, cardStat.modifiers.Count:{cardStat.modifiers.Count}");
                if (j == cardStat.modifiers.Count) txt = "xx";
                else
                {
                    switch (cardStat.modifiers[j].stat)
                    {
                        case Enums.Stats.firerateRatio:
                            txt = $"- Multiplie la vitesse d'attaque par {cardStat.modifiers[0].value}x";
                            break;
                        case Enums.Stats.damageRatio:
                            txt = $"- Multiplie l'attaque par {cardStat.modifiers[0].value}x";
                            break;
                        case Enums.Stats.sizeRatio:
                            txt = $"- Multiplie la taille des balles par {cardStat.modifiers[0].value}x";
                            break;
                        case Enums.Stats.multiCastChance:
                            txt = $"- Augmente les chances de tirs multiple de +{cardStat.modifiers[0].value}%";
                            break;
                        case Enums.Stats.criticalChance:
                            txt = $"- Augmente les chances de coups crtique de +{cardStat.modifiers[0].value}%";
                            break;
                        case Enums.Stats.criticalDamage:
                            txt = $"- Augmente les chances de dégâts critique de +{cardStat.modifiers[0].value}%";
                            break;
                    }
                }
                if (j == 0) cards[i].EffectText1.text = txt;
                else cards[i].EffectText2.text = txt;
            }

        }
    }

    void PlayerMove(int index, bool right)
    {
        int pos = playerPos[index];
        if (pos == -2) return;
        if (right && pos < 4)
        {
            if (pos == 0) startColors[index].SetActive(false);
            else cards[pos - 1].colors[index].SetActive(false);

            cards[pos].colors[index].SetActive(true);
            playerPos[index]++;

        }
        else if (pos > 1)
        {
            cards[pos-1].colors[index].SetActive(false);
            cards[pos-2].colors[index].SetActive(true);
            playerPos[index]--;
        }
    }

    void SelectCard(int index)
    {
        if (!cards[playerPos[index] - 1].selected)
        {
            cards[playerPos[index] - 1].selected = true;
            if (GameManager.Instance != null)
            {
                if (cards[playerPos[index] - 1].isWeapon) ;
                else GameManager.Instance.players[index].GetComponent<ShootController>().AddWeapon(cards[playerPos[index] - 1].weapon);
            }
            cards[playerPos[index] - 1].colors[index].SetActive(false);
            cards[playerPos[index] - 1].back.color = takenColor;
            playerPos[index] = -2;
            cardTake++;
            if(cardTake==4)gameObject.SetActive(false);
        }
    }


}

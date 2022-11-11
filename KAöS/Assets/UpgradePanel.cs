using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    private StatScriptable[] commonCards; 
    private StatScriptable[] uncommonCards; 
    private StatScriptable[] rareCards; 
    private StatScriptable[] epicCards; 
    private StatScriptable[] legendaryCards; 
    
    [Header("=== UI ===")]
    [SerializeField] Color colorcommon;
    [SerializeField] Color colorUncommon;
    [SerializeField] Color colorRare;
    [SerializeField] Color colorEpic;
    [SerializeField] Color colorlegendary;

    [Serializable]
    public struct Card
    {
        public Image back;
        public TextMeshProUGUI EffectText1;
        public TextMeshProUGUI EffectText2;
    }

    public Card[] cards;


    void AssignCards(StatScriptable[] statCards)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            string text = ". . .";
            switch (statCards[i].modifiers[0].stat)
            {
                case Enums.Stats.firerateRatio : text =  $"";
                    break;
            }
        }
    }
}

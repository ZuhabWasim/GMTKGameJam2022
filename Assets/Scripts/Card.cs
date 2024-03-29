using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

[Serializable]
public class Tier
{
    public static int[]
        tierIDs = new int[] {1, 2}; // TODO: Change to 'new int[]{0, 1, 2, 4, 5};' when more cards are developed.

    public static int RESEARCH_TIER = 3;

    public int start;
    public int end; // Exclusive
    public int size;

    public int rarity;
    public string name;

    public List<Card> cardChoices = new();

    public Tier(int tierStart, int tierEnd, int tierRarity, string tierName = null)
    {
        start = tierStart - 1; // This specifies deck indexes so the range is 0-based and end is exclusive.
        end = tierEnd;
        size = end - start;
        rarity = tierRarity;
        name = tierName;

        // Add all of the card choices for each tier.
        GameObject cardBank = GameObject.FindGameObjectWithTag("CardBank");

        foreach (Transform child in cardBank.transform)
        {
            Card card = child.GetComponent<Card>();
            if (card.tier == this.rarity)
            {
                cardChoices.Add(card);
            }
        }
    }

    public Card GetCardChoice(int cardIndex)
    {
        foreach (Card card in cardChoices)
        {
            if (card.id == cardIndex)
            {
                return card;
            }
        }

        return null;
    }

    public Card GetRandomCard()
    {
        int roll = Random.Range(0, cardChoices.Count - 1);

        return cardChoices[roll];
    }

    public Card GetRandomCardWithoutReplacement()
    {
        int roll = Random.Range(0, cardChoices.Count - 1);

        Card card = cardChoices[roll];
        cardChoices.RemoveAt(roll);
        return card;
    }

    public Card GetRandomCardWithReplacement()
    {
        int roll = Random.Range(0, cardChoices.Count - 1);

        Card card = cardChoices[roll];
        return card;
    }

    public static Color32 GetTierColor(int tier)
    {
        switch (tier)
        {
            case 0:
                return new Color32(159, 227, 254, 255);

            case 1:
                return new Color32(247, 252, 168, 255);

            case 2:
                return new Color32(254, 207, 159, 255);

            case 3:
                return new Color32(178, 254, 219, 255);

            case 4:
                return new Color32(223, 155, 253, 255);

            case 5:
                return new Color32(253, 157, 203, 255);

            default:
                return new Color32(180, 180, 180, 255);
        }
    }
}

[Serializable]
public class Card : MonoBehaviour
{
    public Image cardColor;
    public Image img;
    public TMP_Text txtTitle;
    public TMP_Text txtDescription;


    public int id;
    public int tier;

    public string name;
    public string description;


    public int physDamage = 0;
    public int fireDamage = 0;
    public int enerDamage = 0;

    public int physShield = 0;
    public int iceShield = 0;
    public int enerShield = 0;

    public int poison = 0;
    public int sleepy = 0;
    public int fragile = 0;
    public int speed = 0;
    public int strength = 0;
    public int focus = 0;


    public Card(int id = 0, int tier = 0, string name = "", string description = "")
    {
        this.tier = tier;
        this.id = id;
        this.name = name;
        this.description = description;
    }

    void Start()
    {
        //Assign card color based on tier
        cardColor.color = Tier.GetTierColor(tier);
        txtTitle.text = name;
        txtDescription.text = description;
    }

    public static Card GetCardFromBank(int id)
    {
        // Returns the card component directly from the bank, to make sure we're not referencing one that will be destroyed.

        GameObject cardBank = GameObject.FindGameObjectWithTag("CardBank");

        foreach (Transform child in cardBank.transform)
        {
            Card card = child.GetComponent<Card>();
            if (card.id == id)
            {
                return card;
            }
        }

        return null;
    }

    public virtual void ApplyCardEffects()
    {
    }

    protected virtual void CardEffects()
    {
    }
}
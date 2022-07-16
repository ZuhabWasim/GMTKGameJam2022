using System;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public struct Tier
{
    public int start;
    public int end; // Exclusive
    public int size;

    public int rarity;
    public string name;

    public Tier(int tierStart, int tierEnd, int tierRarity, string tierName = null)
    {
        start = tierStart - 1;
        end = tierEnd;
        size = end - start;
        rarity = tierRarity;
        name = tierName;
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


    public int physDamage=0;
    public int fireDamage=0;
    public int enerDamage=0;

    public int physShield=0;
    public int iceShield=0;
    public int enerShield=0;

    public int poison=0;
    public int sleepy=0;
    public int fragile=0;
    public int speed=0;
    public int strength=0;
    public int focus=0;

    
    public Card(int id = 0, int tier = 0, string name = "", string description = "")
    {
        this.tier = tier;
        this.id = id;
        this.name = name;
        this.description = description;
    }

    void Start() {
        //Assign color based on tier
        cardColor.color = new Color32(100, 100, 100, 255);

        txtTitle.text = name;
        txtDescription.text = description;
    }
    
    protected virtual void CardEffects()
    {
    }
}
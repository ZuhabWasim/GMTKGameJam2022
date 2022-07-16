using System;
using UnityEngine;

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
    public int id;
    public int tier;

    public string name;
    public string description;
    
    public Card(int id = 0, int tier = 0, string name = "", string description = "")
    {
        this.tier = tier;
        this.id = id;
        this.name = name;
        this.description = description;
    }
    
    protected virtual void CardEffects()
    {
    }
}
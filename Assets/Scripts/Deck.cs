using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public static int DECK_SIZE = 12;
    public static int RESEARCH_CARD_INDEX = 6;
    public static int RESEARCH_TIER_INDEX = 3;
    public static int STARTING_CARDS = 6;

    [SerializeField] private List<Tier> _tiers = new();

    public List<Card> _deck = new List<Card>(DECK_SIZE);

    private void Start()
    {
        InitializeTiers();
        InitializeDeck();
        PickStartingCards();
    }

    private void PickStartingCards()
    {
        // Generate a random cards for tier 0, 1, and 2. Place them in the tier in the deck

        foreach (Tier tier in _tiers)
        {
            if (tier.end <= STARTING_CARDS)
            {
                for (int i = tier.start; i < tier.end; i++)
                {
                    _deck[i] = tier.GetRandomCardWithoutReplacement();
                }
            }
        }
        
        // Get the research card.
        _deck[RESEARCH_CARD_INDEX] = _tiers[RESEARCH_TIER_INDEX].GetRandomCard(); // Research Card
        
    }

    private void InitializeDeck()
    {
        for (int i = 0; i < DECK_SIZE; i++) _deck.Add(null);
    }

    private void InitializeTiers()
    {
        _tiers.Add(new Tier(1, 1, 0, "Default"));
        _tiers.Add(new Tier(2, 4, 1, "Rare"));
        _tiers.Add(new Tier(5, 6, 2, "Epic"));
        
        // Research Tier (make sure to update the RESEARCH_TIER constant.
        _tiers.Add(new Tier(7, 7, 3, "Research"));
        
        _tiers.Add(new Tier(8, 10, 4, "Unique"));
        _tiers.Add(new Tier(11, 12, 5, "Legendary"));
    }

    public Card GetCard(int i)
    {
        if (i >= _deck.Count) Debug.LogError("Invalid index i used for accessing cards.");
        return _deck[i];
    }

    public List<Card> GetCards()
    {
        return _deck;
    }

    public void SetCard(int i, Card newCard)
    {
        _deck[i] = newCard;
    }

    public void SetCards(List<Card> newCards)
    {
        _deck = newCards;
    }

    // To access the Tier (range of cards) a new card belongs to.
    public Tier? getCardTier(Card card)
    {
        foreach (var tier in _tiers)
            if (tier.rarity == card.tier)
                return tier;
        return null;
    }

    public int getDeckIndex(Card card)
    {
        for (var i = 0; i < DECK_SIZE; i++)
            if (_deck[i] != null && _deck[i].id == card.id)
                return i;

        return -1;
    }

    // Whether the player can craft a card in the sum they'd like.
    public bool isCraftableCard(int sum)
    {
        return _deck[sum] != null;
    }

    public Card researchCardFromTier(int tierID)
    {
        return _tiers[tierID].GetRandomCardWithoutReplacement();
    }
}
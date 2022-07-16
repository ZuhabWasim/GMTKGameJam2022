using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public static int DECK_SIZE = 12;
    public static int RESEARCH_CARD = 6;
    public static int RESEARCH_TIER = 3;
    public static int STARTING_CARDS = 6;

    public List<Tier> _tiers = new();

    public List<Card> _deck = new(DECK_SIZE);

    private void Start()
    {
        InitializeTiers();
        InitializeDeck();
        PickStartingCards();
    }

    private void PickStartingCards()
    {
        var tempID = 1;
        // Generate a random cards for tier 0, 1, and 2. Place them in the tier in the deck
        foreach (var tier in _tiers)
            if (tier.end <= STARTING_CARDS)
                for (var i = tier.start; i < tier.end; i++)
                {
                    // TODO: Generate a random card of the specific tiers.
                    _deck[i] = new Card(tempID, tier.rarity, "Card Name", "Card Description");
                    tempID += 1;
                }
    }

    private void InitializeDeck()
    {
        for (var i = 0; i < DECK_SIZE; i++) _deck.Add(null);
    }

    private void InitializeTiers()
    {
        _tiers.Add(new Tier(1, 1, 0, "Default"));
        _tiers.Add(new Tier(2, 4, 1, "Rare"));
        _tiers.Add(new Tier(5, 6, 2, "Epic"));
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
}
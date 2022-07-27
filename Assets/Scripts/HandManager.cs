using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class HandManager : MonoBehaviour
{
    public static int HAND_SIZE = 5;
    public static int PLAY_SIZE = 3;
    
    // TODO: See if i should replace each individual card stack with a list of lists.
    public static int HAND = 0;
    public static int CRAFTING = 1;
    public static int PLAYING = 2;
    public static int RESEARCHING = 3;

    public Deck deck;
    public bool isResearching = false;
    
    [SerializeField] public CardStack crafting;
    [SerializeField] public CardStack hand;
    [SerializeField] public CardStack playing;
    [SerializeField] public CardStack researching;

    void Start()
    {
        CardStack[] cardStacks = GetComponents<CardStack>();

        for (int i = 0; i < cardStacks.Length; i++)
        {
            if (cardStacks[i].dropZoneType == DropZoneType.UNASSIGNED)
            {
                Debug.LogError("Card stack in " + this.gameObject.name + " is unnasigned.");
            }
        }
    }

    public int getCraftableIndex()
    {
        var sum = 0;
        foreach (Card card in crafting.stack) sum += deck.getDeckIndex(card);

        return sum;
    }

    public bool CraftCard()
    {
        var index = getCraftableIndex(); // The sum of all the crafting cards.

        if (researching.Size() > 0)
        {
            return false;
        }
        
        if (deck.isCraftableCard(index))
        {
            if (index == Deck.RESEARCH_CARD_INDEX)
            {
                Research();
                // TODO: Researching functionality.
                // Generate a random card of any tier but the research tier
                // Force player to select where they want the new card to be
                //DrawCard(index);
            }
            else
            {
                DrawCard(index);
            }

            crafting.clearStack();
            return true;
        }

        return false;
    }

    public bool DrawCard(int deckIndex, bool generate = false)
    {
        if (hand.Size() == HAND_SIZE) return false;

        Card card = deck.GetCard(deckIndex - 1);
        hand.addToStack(card, generate);
        return true;
    }

    public void Research()
    {
        // Generate a random tier and random card (that's not the research tier).
        int tierID = Tier.TIER_IDS[Random.Range(0, Tier.TIER_IDS.Length)];
        Card card = deck.researchCardFromTier(tierID);
        
        researching.addToStack(card);

        isResearching = true;

    }

    public void ClearStacks()
    {
        hand.clearStack();
        crafting.clearStack();
        playing.clearStack();
        researching.clearStack();
    }
}
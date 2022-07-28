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

    public void Load()
    {
        
    }
    
    public int getCraftableIndex()
    {
        // zero-based

        var sum = 0;
        foreach (Card card in crafting.stack)
        {
            sum += deck.getDeckIndex(card) + 1;
        }

        return sum - 1;
    }

    public bool CraftCard()
    {
        if (crafting.isEmpty()) return false;

        var index = getCraftableIndex(); // The sum of all the crafting cards 0-based.

        if (isResearching) return false;

        if (deck.isCraftableCard(index)) // 0-based.
        {
            if (index == Deck.RESEARCH_CARD_INDEX)
            {
                Research();
            }
            else
            {
                DrawCard(index, generate: true);
            }

            crafting.clearStack();
            return true;
        }

        return false;
    }

    public bool DrawCard(int deckIndex, bool generate = false)
    {
        // 0-based
        if (hand.Size() == HAND_SIZE) return false;

        Card card = deck.GetCard(deckIndex);
        hand.addToStack(card, generate);
        return true;
    }

    public void Research()
    {
        // Generate a random tier and random card (that's not the research tier).
        int roll = Random.Range(0, Tier.tierIDs.Length);
        int tierID = Tier.tierIDs[roll];
        Card card = deck.researchCardFromTier(tierID);

        researching.addToStack(card, generate: true);
    }

    public void ClearStacks()
    {
        hand.clearStack();
        crafting.clearStack();
        playing.clearStack();
        researching.clearStack();
    }

    void Update()
    {
        isResearching = !researching.isEmpty();
    }
}
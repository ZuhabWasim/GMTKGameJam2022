using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static int HAND_SIZE = 5;
    public static int PLAY_SIZE = 3;

    public Deck deck;
    
    [SerializeField] public CardStack crafting;
    [SerializeField] public CardStack hand;
    [SerializeField] public CardStack playing;
    void Start()
    {
        CardStack[] cardStacks = GetComponents<CardStack>();

        for (int i = 0; i < cardStacks.Length; i++)
        {
            if (cardStacks[i].dropZoneType == DropZoneType.HAND)
            {
                hand = cardStacks[i];
            } else if (cardStacks[i].dropZoneType == DropZoneType.CRAFTING)
            {
                crafting = cardStacks[i];
            } else if (cardStacks[i].dropZoneType == DropZoneType.PLAYING)
            {
                playing = cardStacks[i];
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
        var index = getCraftableIndex();
        if (deck.isCraftableCard(index))
        {
            if (index == Deck.RESEARCH_CARD)
                // TODO: Researching functionality.
                // Generate a random card of any tier but the research tier
                // Force player to select where they want the new card to be
                DrawCard(index);
            else
                DrawCard(index);

            crafting.clearStack();
            return true;
        }

        return false;
    }

    public bool DrawCard(int deckIndex)
    {
        if (hand.Size() == HAND_SIZE) return false;
        
        Card card = deck.GetCard(deckIndex);
        hand.addToStack(card);
        return true;
    }
}
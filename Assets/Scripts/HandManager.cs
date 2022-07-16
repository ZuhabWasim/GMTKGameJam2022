using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static int HAND_SIZE = 5;
    public static int PLAY_SIZE = 3;

    public Deck deck;
    [SerializeField] public List<Card> crafting = new();
    [SerializeField] public List<Card> hand = new(HAND_SIZE);
    [SerializeField] public List<Card> playing = new();


    public bool moveCardtoCrafting(int cardIndex)
    {
        if (cardIndex >= HAND_SIZE) return false;

        var card = hand[cardIndex];

        //crafting.Push(card);
        crafting.Add(card);

        hand.RemoveAt(cardIndex);

        return true;
    }

    public bool moveCardtoPlaying(int cardIndex)
    {
        if (cardIndex >= HAND_SIZE) return false;
        if (playing.Count == PLAY_SIZE) return false;

        var card = hand[cardIndex];

        //playing.Push(card);
        playing.Add(card);

        hand.RemoveAt(cardIndex);

        return true;
    }

    public void returnCardFromCrafting()
    {
        //Card card = crafting.Pop();
        var card = crafting[^1];
        crafting.RemoveAt(crafting.Count - 1);

        hand.Add(card);
    }

    public void returnCardFromPlaying()
    {
        /*Card card = playing.Pop();*/
        var card = playing[^1];
        playing.RemoveAt(playing.Count - 1);

        hand.Add(card);
    }

    public int getCraftableIndex()
    {
        var sum = 0;
        foreach (var card in crafting) sum += deck.getDeckIndex(card);

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

            crafting.Clear();
            return true;
        }

        return false;
    }

    public bool DrawCard(int deckIndex)
    {
        if (hand.Count == HAND_SIZE) return false;
        
        Card card = deck.GetCard(deckIndex);
        hand.Add(card);
        return true;
    }
}
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class HandManager : MonoBehaviour
{
    public static int HAND_SIZE = 5;
    public static int PLAY_SIZE = 3;

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

    public int GetCraftableIndex()
    {
        var sum = 0;
        foreach (Card card in crafting.stack)
        {
            sum += deck.GetDeckIndex(card) + 1;
        }

        return sum - 1; // zero-based
    }

    public bool CraftCard()
    {
        if (crafting.IsEmpty()) return false;

        var index = GetCraftableIndex(); // The sum of all the crafting cards 0-based.

        if (isResearching) return false;

        if (deck.IsCraftableCard(index)) // 0-based.
        {
            if (index == Deck.RESEARCH_CARD_INDEX)
            {
                Research();
            }
            else
            {
                DrawCard(index, generate: true);
            }

            crafting.ClearStack();
            return true;
        }

        return false;
    }

    public bool DrawCard(int deckIndex, bool generate = false)
    {
        if (hand.Size() == HAND_SIZE) return false;

        Card card = deck.GetCard(deckIndex); // 0-based
        hand.AddToStack(card, generate);
        return true;
    }

    public void Research()
    {
        // Generate a random tier and random card (that's not the research tier).
        int roll = Random.Range(0, Tier.tierIDs.Length);
        int tierID = Tier.tierIDs[roll];
        Card card = deck.ResearchCardFromTier(tierID);

        researching.AddToStack(card, generate: true);
    }

    public void ClearStacks()
    {
        hand.ClearStack();
        crafting.ClearStack();
        playing.ClearStack();
        researching.ClearStack();
    }

    void Update()
    {
        isResearching = !researching.IsEmpty();
    }
}
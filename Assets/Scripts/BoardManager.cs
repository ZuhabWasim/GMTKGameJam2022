using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DropZoneType
{
    UNASSIGNED,
    CRAFTING,
    PLAYING,
    HAND,
    RESEARCHING,
    DECK_SLOT
}

public class BoardManager : MonoBehaviour
{
    [SerializeField] private Player player1;
    [SerializeField] private Player player2;


    // Start is called before the first frame update
    void Start()
    {
        // By default set player1 to go first
        player1.turn = true;

        //UpdateBoard();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public Player getPlayerTurn()
    {
        return player1.turn ? player1 : player2;
    }

    public void DrawCards()
    {
        getPlayerTurn().rollForCards();
    }

    public void CraftCards()
    {
        getPlayerTurn().hand.CraftCard();
    }

    public void WipeBoard()
    {
        getPlayerTurn().WipeBoard();
    }

    public void UpdateBoard()
    {
        Player player = getPlayerTurn();
    }

    public void UpdateCardStack(CardStack cardStack, string cardStackTag)
    {
        GameObject cardStackObj = GameObject.FindGameObjectWithTag(cardStackTag);

        foreach (Card card in cardStack.stack)
        {
            // If this Card isn't in the list
        }

        if (cardStackObj.transform.childCount == cardStack.Size())
        {
        }
    }
}
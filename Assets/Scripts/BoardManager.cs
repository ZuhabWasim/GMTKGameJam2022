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
        UpdateBoardTurn();
    }

    public Player GetPlayerTurn()
    {
        return player1.turn ? player1 : player2;
    }

    public void ChangePlayerTurn()
    {
        player1.turn = !player1.turn;
        player2.turn = !player2.turn;
        UpdateBoardTurn(); // Updates the board along with the turn.
    }

    public void DrawCards()
    {
        GetPlayerTurn().RollForCards();
    }

    public void CraftCards()
    {
        GetPlayerTurn().hand.CraftCard();
    }

    public void WipeBoard()
    {
        GetPlayerTurn().WipeBoard();
    }

    public void UpdateBoardTurn()
    {
        // Only updates the board based off of the current player (doesn't change the turn).
        Player player = GetPlayerTurn();
        player.LoadPlayer();

        // Update all drop zones objects (hard stack, craft stack) to reflect this player's stacks
        foreach (DropZone dropZone in
                 GameObject.FindGameObjectWithTag("CardStacks").GetComponentsInChildren<DropZone>())
        {
            dropZone.LoadPlayerStacks();
        }

        // Update all display components to update on this new player's stack changes.
        foreach (CardStackDisplay cardStackDisplay in
                 GameObject.FindGameObjectWithTag("CardStacks").GetComponentsInChildren<CardStackDisplay>())
        {
            cardStackDisplay.LoadCardStackLinks();
        }
    }
}
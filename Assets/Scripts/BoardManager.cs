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

    // Update is called once per frame
    void Update()
    {
    }

    public Player getPlayerTurn()
    {
        return player1.turn ? player1 : player2;
    }

    public void ChangePlayerTurn()
    {
        player1.turn = !player1.turn;
        player2.turn = !player2.turn;
        UpdateBoardTurn();
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

    public void UpdateBoardTurn()
    {
        Player player = getPlayerTurn();
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
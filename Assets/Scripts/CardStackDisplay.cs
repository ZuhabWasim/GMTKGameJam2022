using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum CounterType
{
    CardCount,
    CraftableIndex
}

public class CardStackDisplay : MonoBehaviour
{
    public CardStack cardStack;
    public DropZoneType dropZonetype;

    public TextMeshProUGUI cardCounter;
    public TextMeshProUGUI cardMax;
    public Button button;

    public CounterType counterType;

    public void LoadCardStackLinks()
    {
        // Assign the card stack with which ever player's turn it is
        cardStack = null;
        Player player = GameObject.FindGameObjectWithTag("BoardManager").GetComponent<BoardManager>().GetPlayerTurn();
        foreach (CardStack playerCardStack in player.GetComponentsInChildren<CardStack>())
        {
            if (playerCardStack.dropZoneType == this.dropZonetype)
            {
                cardStack = playerCardStack;
            }
        }

        if (cardStack == null)
        {
            Debug.LogError("No card stack defined in " + this.gameObject.name);
            return;
        }

        cardStack.StackChanged += OnStackChanged; // TODO: might cause issues with added subscribing.

        if (cardCounter != null)
        {
            cardCounter.text = cardStack.Size() + "";
        }

        if (cardMax != null && cardStack.isLimited)
        {
            cardMax.text = "/" + cardStack.stackLimit;
        }

        OnStackChanged(null);
    }

    void OnStackChanged(Card card)
    {
        switch (counterType)
        {
            case CounterType.CardCount:
                cardCounter.text = cardStack.Size() + "";
                return;
            case CounterType.CraftableIndex:
                Player player = GameObject.FindGameObjectWithTag("BoardManager").GetComponent<BoardManager>()
                    .GetPlayerTurn();
                if (player == null)
                {
                    cardCounter.text = "0";
                }
                else
                {
                    cardCounter.text = player.hand.GetCraftableIndex() + 1 + "";
                }

                return;
            default:
                cardCounter.text = cardStack.Size() + "";
                return;
        }
    }
}
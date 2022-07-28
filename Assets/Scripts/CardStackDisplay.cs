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
    
    public TextMeshProUGUI cardCounter;
    public TextMeshProUGUI cardMax;
    public Button button;

    public CounterType counterType;
    
    // Start is called before the first frame update
    void Start()
    {
        
        if (cardStack == null)
        {
            Debug.LogError("No card stack defined in " + this.gameObject.name);
            return;
        }
        
        cardStack.StackChanged += OnStackChanged;
        
        if (cardCounter != null)
        {
            cardCounter.text = cardStack.Size() + "";
        }

        if (cardMax != null && cardStack.isLimited)
        {
            cardMax.text = "/" + cardStack.stackLimit;
        }
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
                    .getPlayerTurn();
                if (player == null)
                {
                    cardCounter.text = "0";
                }
                else
                {
                    cardCounter.text = player.hand.getCraftableIndex() + 1 + "";
                }
                return;
            default:
                cardCounter.text = cardStack.Size() + "";
                return;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

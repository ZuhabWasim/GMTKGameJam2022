using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DeckSlot : MonoBehaviour
{
    private bool _mouseOver = false;
    
    public int deckSlotID;
    public int deckSlotTier;
    
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI cardDesc;
    public TextMeshProUGUI id;
    
    public Card card;
    
    [Header("Text Game Objects")]
    public GameObject nameObj;
    public GameObject descObj;
    public GameObject idObj;

    
    
    // Start is called before the first frame update
    void Start()
    {
        cardName = nameObj.GetComponent<TextMeshProUGUI>();
        cardDesc = descObj.GetComponent<TextMeshProUGUI>();
        
        id = idObj.GetComponent<TextMeshProUGUI>();
        id.text = deckSlotID + "";

        GetComponent<Image>().color = Tier.GetTierColor(deckSlotTier);

        if (card == null)
        {
            cardName.text = "";
            cardDesc.text = "";
        }

    }

    public void UpdateDeckSlot(int id)
    {
        // Updates the deck along with the slot (used when researching).
        card = Card.GetCardFromBank(id);
        cardName.text = card.name;
        
        Deck deck = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<Deck>();
        deck.SetCard(deckSlotID - 1, card);
        
    }
    
    public void SetDeckSlot(int id)
    {
        // Sets the information without updating the deck itself.
        card = Card.GetCardFromBank(id);
        cardName.text = card.name;
    }

    public void OnPointerEnter()
    {
        _mouseOver = true;
    }

    public void OnPointerExit()
    {
        _mouseOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_mouseOver && card != null)
        {
            cardDesc.text = card.description;
        }
        else
        {
            cardDesc.text = "";
        }
    }
}

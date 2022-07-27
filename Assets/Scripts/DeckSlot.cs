using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DeckSlot : MonoBehaviour
{
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

    }

    public void UpdateDeckSlot(int id)
    {
        card = Card.GetCardFromBank(id);
        cardName.text = card.name;
        cardDesc.text = card.description;
    }

    public void OnPointerEnter()
    {
        cardDesc.text = (card != null) ? card.description : "Test";
    }

    public void OnPointerExit()
    {
        cardDesc.text = "";
    }

    // Update is called once per frame
    void Update()
    {
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    public DropZoneType dropZonetype;
    public CardStack cardStack;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
    public void OnDrop(PointerEventData eventData)
    {
        Card card = eventData.pointerDrag.GetComponent<Card>();
        Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();

        if (draggable.sourceDropZone.dropZonetype == dropZonetype) return;
        if (!draggable.sourceDropZone.cardStack.moveToNewStack(cardStack, card.id)) return;

        Debug.Log("Card " + card.name + " (" + card.id + ") was dropped from " + draggable.sourceDropZone.dropZonetype + " to " + dropZonetype);
        
        draggable.UpdateDropZone(this);
        
        /*Debug.Log("Updating card " + card.name + " (" + card.id + ") to belong in " + dropZonetype);*/

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        
        if (d != null)
        {
            d.parentToReturnTo = this.transform;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        cardStack = GetComponent<CardStack>();

        foreach (Transform child in transform)
        {
            Card card = child.GetComponent<Card>();
            cardStack.addToStack(card);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

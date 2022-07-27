using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public DropZoneType dropZonetype;
    public CardStack cardStack;

    public List<DropZone> allowableDropZones = new();
    public List<DropZone> deniedDropZones = new();

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Debug.Log("OnPointerEnter");
        if (eventData.pointerDrag == null) return;

        // Don't update the placeholder to show that it's not possible to place in the stack.
        if (!Droppable(eventData)) return;


        // Update the placeholder to go into different stacks.
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

        if (d != null)
        {
            d.placeholderParent = this.transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Debug.Log("OnPointerExit");
        if (eventData.pointerDrag == null) return;

        // Update the placeholder back to the original place if the player didn't choose any other stack.
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

        if (d != null && d.placeholderParent == this.transform)
        {
            d.placeholderParent = d.parentToReturnTo;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Card card = eventData.pointerDrag.GetComponent<Card>();
        Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();

        // Place the card on the new stack if it's possible.
        if (!Droppable(eventData)) return;

        // Deck slots will have the card "used up" to be inserted into the slot.
        if (dropZonetype == DropZoneType.DECK_SLOT)
        {
            DeckSlot deckSlot = GetComponent<DeckSlot>();
            deckSlot.UpdateDeckSlot(card.id);
            draggable.sourceDropZone.cardStack.removeFromStack(card);

            // TODO: Probably don't need this other draggable
            Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

            if (d != null)
            {
                Destroy(d.gameObject);
            }
        }
        // Otherwise we put cards in the d
        else
        {
            draggable.sourceDropZone.cardStack.moveToNewStack(cardStack, card.id);

            Debug.Log("Card " + card.name + " (" + card.id + ") was dropped from " +
                      draggable.sourceDropZone.dropZonetype +
                      " to " + dropZonetype);

            draggable.UpdateDropZone(this);

            // TODO: Probably don't need this other draggable
            Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

            if (d != null)
            {
                d.parentToReturnTo = this.transform;
            }
        }
    }

    bool Droppable(PointerEventData eventData)
    {
        Card card = eventData.pointerDrag.GetComponent<Card>();
        Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();

        // Cards can't be dropped in the same stack. Don't let it drop.
        if (draggable.sourceDropZone.dropZonetype == dropZonetype) return false;

        // If allow list has drop zones, and the Card's source isn't in it. Don't let it drop.
        if (allowableDropZones.Count != 0 && !allowableDropZones.Contains(draggable.sourceDropZone)) return false;

        // If the deny list has drop zones, and the Card's source is in it. Don't let it drop.
        if (deniedDropZones.Count != 0 && deniedDropZones.Contains(draggable.sourceDropZone)) return false;

        // Checks whether the card can be placed in that specified deck slot.
        if (dropZonetype == DropZoneType.DECK_SLOT)
        {
            DeckSlot deckSlot = GetComponent<DeckSlot>();
            return deckSlot.deckSlotTier == card.tier;
        }

        // If the card has a stack and it's full or any other reason, don't allow placing.
        if (!draggable.sourceDropZone.cardStack.canMoveToNewStack(cardStack, card.id)) return false;

        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        cardStack = GetComponent<CardStack>();

        if (dropZonetype != DropZoneType.DECK_SLOT)
        {
            foreach (Transform child in transform)
            {
                Card card = child.GetComponent<Card>();
                cardStack.addToStack(card);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
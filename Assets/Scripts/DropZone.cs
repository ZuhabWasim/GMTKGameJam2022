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

        if (draggable == null) return;

        // Place the card on the new stack if it's possible.
        if (!Droppable(eventData)) return;

        // Deck slots will have the card "used up" to be inserted into the slot.
        if (dropZonetype == DropZoneType.DECK_SLOT)
        {
            DeckSlot deckSlot = GetComponent<DeckSlot>();
            deckSlot.UpdateDeckSlot(card.id);
            draggable.sourceDropZone.cardStack.RemoveFromStack(card);

            // Destroy the draggable when updating the deck slot.
            Destroy(draggable.gameObject);
        }
        // Otherwise we put cards in the other dropzone.
        else
        {
            draggable.sourceDropZone.cardStack.MoveToNewStack(cardStack, card.id);

            Debug.Log("Card " + card.name + " (" + card.id + ") was dropped from " +
                      draggable.sourceDropZone.dropZonetype +
                      " to " + dropZonetype);

            draggable.UpdateDropZone(this);

            // Update the parent to the new dropzone.
            draggable.parentToReturnTo = this.transform;
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
        if (!draggable.sourceDropZone.cardStack.CanMoveToNewStack(cardStack, card.id)) return false;

        return true;
    }

    public void LoadPlayerStacks()
    {
        if (dropZonetype == DropZoneType.DECK_SLOT) return;

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

        // Remove all of the cards from the previous player
        cardStack.ClearStackObjects();

        // and put in the next players cards from their stacks.
        cardStack.AddAllStackObjects();
    }
}
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
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public void OnDrop(PointerEventData eventData)
    {
        Card card = eventData.pointerDrag.GetComponent<Card>();
        Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();

        // Cards can't be dropped in the same stack. Don't let it drop.
        if (draggable.sourceDropZone.dropZonetype == dropZonetype) return;

        // If allow list has drop zones, and the Card's source isn't in it. Don't let it drop.
        if (allowableDropZones.Count != 0 && !allowableDropZones.Contains(draggable.sourceDropZone)) return;

        // If the deny list has drop zones, and the Card's source is in it. Don't let it drop.
        if (deniedDropZones.Count != 0 && deniedDropZones.Contains(draggable.sourceDropZone)) return;

        // If the card stack is full or any other reason, don't allow placing.
        if (!draggable.sourceDropZone.cardStack.moveToNewStack(cardStack, card.id)) return;

        Debug.Log("Card " + card.name + " (" + card.id + ") was dropped from " + draggable.sourceDropZone.dropZonetype +
                  " to " + dropZonetype);

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
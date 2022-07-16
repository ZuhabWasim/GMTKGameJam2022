using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    public DropZoneType dropZonetype;

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject cardObj = eventData.pointerDrag;
        Card card = cardObj.GetComponent<Card>();
        
        
        Debug.Log("Card " + card.name + " with ID " + card.id + " was dropped onto " + gameObject.name);

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        
        if (d != null)
        {
            d.parentToReturnTo = this.transform;
        }
        
        
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

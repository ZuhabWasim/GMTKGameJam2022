using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform parentToReturnTo = null;
    public Transform placeholderParent = null;
    
    public DropZone sourceDropZone;

    private GameObject placeholder = null;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        /*Debug.Log("On Begin Drag");*/

        CreatePlaceholder();

        parentToReturnTo = this.transform.parent;
        placeholderParent = parentToReturnTo;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("On Drag");
        this.transform.position = eventData.position;

        if (placeholder.transform.parent != placeholderParent)
        {
            placeholder.transform.SetParent(placeholderParent);
        }
        
        int newSiblingIndex = placeholderParent.childCount;
        
        for (int i = 0; i < placeholderParent.childCount; i++)
        {
            if (this.transform.position.x < placeholderParent.GetChild(i).position.x)
            {
                newSiblingIndex = i;
                
                // Ignores the placeholder to avoid glitching on sides.
                if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                {
                    newSiblingIndex--;
                }
                break;
            }
        }
        
        placeholder.transform.SetSiblingIndex(newSiblingIndex);
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        /*Debug.Log("On End Drag");*/

        this.transform.SetParent(parentToReturnTo);
        this.transform.SetSiblingIndex( placeholder.transform.GetSiblingIndex());
        
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        
        Destroy(placeholder);
    }

    void CreatePlaceholder()
    {
        placeholder = new GameObject();
        placeholder.name = "Placeholder Card";
        placeholder.transform.SetParent( this.transform.parent );
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleWidth = 0;
        
        // To ensure where you picked up the card is how you place it.
        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateDropZone(this.transform.parent.GetComponent<DropZone>());
    }

    public void UpdateDropZone(DropZone dropZone)
    {
        sourceDropZone = dropZone;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
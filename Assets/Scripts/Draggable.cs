using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform parentToReturnTo = null;
    public DropZone sourceDropZone;

    public void OnBeginDrag(PointerEventData eventData)
    {
        /*Debug.Log("On Begin Drag");*/

        parentToReturnTo = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("On Drag");
        this.transform.position = eventData.position;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        /*Debug.Log("On End Drag");*/

        this.transform.SetParent(parentToReturnTo);

        GetComponent<CanvasGroup>().blocksRaycasts = true;
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
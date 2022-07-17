using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CardStack : MonoBehaviour
{
    public DropZoneType dropZoneType;

    public bool isLimited = false;
    public int stackLimit;

    // These are lists and not stacks because stacks don't show up in the inspector smh
    [SerializeField] public List<Card> stack = new();

    // Start is called before the first frame update
    void Start()
    {
        DropZone dropZone = GetComponent<DropZone>();
        if (dropZone != null)
        {
            dropZoneType = dropZone.dropZonetype;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool moveToNewStack(CardStack newStack, int cardID)
    {
        for (int i = 0; i < stack.Count; i++)
            if (stack[i].id == cardID)
            {
                if (!newStack.isFull())
                {
                    Card temp = stack[i];
                    newStack.stack.Add(temp);
                    stack.RemoveAt(i);
                    return true;
                }
            }

        return false;
    }

    public bool addToStack(Card card)
    {
        if (isFull()) return false;
        stack.Add(card);
        return true;
    }

    public bool removeFromStack(Card card)
    {
        for (int i = 0; i < stack.Count; i++)
            if (stack[i].id == card.id)
            {
                stack.RemoveAt(i);
                return true;
            }

        return false;
    }

    public bool isFull()
    {
        return (isLimited && stack.Count >= stackLimit);
    }

    public int Size()
    {
        return stack.Count;
    }

    public void clearStack()
    {
        stack.Clear();
    }
    
}
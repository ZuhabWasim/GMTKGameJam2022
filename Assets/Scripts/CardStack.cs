using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = System.Object;

[Serializable]
public class CardStack : MonoBehaviour
{
    public DropZoneType dropZoneType;

    public bool isLimited = false;
    public int stackLimit;

    // These are lists and not stacks because stacks don't show up in the inspector smh
    [SerializeField] public List<Card> stack = new();

    public delegate void OnStackChanged(Card card);
    public event OnStackChanged StackChanged;

    // Start is called before the first frame update
    void Start()
    {
        DropZone dropZone = GetComponent<DropZone>();
        if (dropZone != null)
        {
            dropZoneType = dropZone.dropZonetype;
        }

        clearStack();
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
                    newStack.addToStack(temp);
                    stack.RemoveAt(i);
                    StackChanged?.Invoke(temp);
                    return true;
                }
            }

        return false;
    }

    public bool canMoveToNewStack(CardStack newStack, int cardID)
    {
        for (int i = 0; i < stack.Count; i++)
            if (stack[i].id == cardID)
            {
                if (!newStack.isFull())
                {
                    return true;
                }
            }

        return false;
    }

    public bool addToStack(Card card, bool generate = false)
    {
        if (isFull()) return false;
        stack.Add(card);
        if (generate)
        {
            Instantiate(Card.GetCardFromBank(card.id).gameObject, this.transform);
        }
        StackChanged?.Invoke(card);

        return true;
    }

    public bool removeFromStack(Card card)
    {
        // Representation removal.
        for (int i = 0; i < stack.Count; i++)
        {
            if (stack[i].id == card.id)
            {
                Card temp = stack[i];
                stack.RemoveAt(i);
                StackChanged?.Invoke(temp);
                return true;
            }
        }

        return false;
    }

    public bool isFull()
    {
        return (isLimited && stack.Count >= stackLimit);
    }

    public bool isEmpty()
    {
        return stack.Count == 0;
    }

    public int Size()
    {
        return stack.Count;
    }

    public void clearStack()
    {
        // Representation clearing.
        stack.Clear();

        // Game objects clearing.
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
        StackChanged?.Invoke(null);
    }
}
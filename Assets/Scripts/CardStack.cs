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
    public GameObject stackObj;

    public bool isLimited = false;
    public int stackLimit;

    // These are lists and not stacks because stacks don't show up in the inspector smh
    [SerializeField] public List<Card> stack = new();

    public delegate void OnStackChanged(Card card);

    public event OnStackChanged StackChanged;

    // Start is called before the first frame update
    void Start()
    {
        if (stackObj == null)
        {
            Debug.LogWarning("No stack object specified, using this stack's object instead.");
            stackObj = this.gameObject;
        }
    }

    public bool MoveToNewStack(CardStack newStack, int cardID)
    {
        for (int i = 0; i < stack.Count; i++)
            if (stack[i].id == cardID && !newStack.IsFull())
            {
                Card temp = stack[i];
                newStack.AddToStack(temp);
                stack.RemoveAt(i);
                StackChanged?.Invoke(temp);
                return true;
            }

        return false;
    }

    public bool CanMoveToNewStack(CardStack newStack, int cardID)
    {
        for (int i = 0; i < stack.Count; i++)
            if (stack[i].id == cardID && !newStack.IsFull())
            {
                return true;
            }

        return false;
    }

    public bool AddToStack(Card card, bool generate = false)
    {
        if (IsFull()) return false;
        stack.Add(card);
        if (generate)
        {
            Instantiate(Card.GetCardFromBank(card.id).gameObject, stackObj.transform);
        }

        StackChanged?.Invoke(card);

        return true;
    }

    public bool RemoveFromStack(Card card)
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

    public bool IsFull()
    {
        return (isLimited && stack.Count >= stackLimit);
    }

    public bool IsEmpty()
    {
        return stack.Count == 0;
    }

    public int Size()
    {
        return stack.Count;
    }

    public void ClearStack()
    {
        // Representation clearing.
        stack.Clear();

        // Game objects clearing.
        foreach (Transform child in stackObj.transform)
        {
            Destroy(child.gameObject);
        }

        StackChanged?.Invoke(null);
    }

    public void ClearStackObjects()
    {
        // Game objects clearing.
        foreach (Transform child in stackObj.transform)
        {
            Destroy(child.gameObject);
        }

        StackChanged?.Invoke(null);
    }

    public void AddAllStackObjects()
    {
        // Assume that all objects have been cleared (so no artifacts remain).
        foreach (Card card in stack)
        {
            Instantiate(Card.GetCardFromBank(card.id).gameObject, stackObj.transform);
        }
    }

    public void SetStack(List<Card> newStack)
    {
        this.stack = newStack;
    }
}
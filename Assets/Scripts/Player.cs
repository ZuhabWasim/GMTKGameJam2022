using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public PlayerStatus status;
    [SerializeField] public ShieldList shields;

    public bool turn = false;

    [SerializeField] public Deck deck;
    [SerializeField] public HandManager hand;

    public int dieCount = 3;
    public int MIN_ROLL = 1;
    public int MAX_ROLL = 6;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void playCards()
    {
        foreach (Card card in hand.playing.stack)
        {
            card.ApplyCardEffects();
        }
    }

    public int rollForCards()
    {
        int drawn = 0;
        for (int i = 0; i < dieCount; i++)
        {
            int roll = Random.Range(MIN_ROLL - 1, MAX_ROLL); // D6 Roll
            if (!hand.DrawCard(roll, true)) break;
            drawn += 1;
        }

        return drawn;
    }

    public void WipeBoard()
    {
        hand.ClearStacks();
    }
}
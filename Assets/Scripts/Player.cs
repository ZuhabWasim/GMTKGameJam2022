using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStatus status;
    [SerializeField] private ShieldList shields;
    
    public bool turn = false;
    
    [SerializeField] private Deck deck;
    [SerializeField] private HandManager hand;

    public int dieCount = 3;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int rollForCards()
    {
        int drawn = 0;
        for (int i = 0; i < dieCount; i++)
        {
            int roll = Random.Range(0, 5);
            if (!hand.DrawCard(roll)) break;
            drawn += 1;
        }

        return drawn;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField]
    private Player player1;
    [SerializeField]
    private Player player2;
    
    // Start is called before the first frame update
    void Start()
    {
        // By default set player1 to go first
        player1.turn = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

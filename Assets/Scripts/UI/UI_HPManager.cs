using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UI_HPManager : MonoBehaviour
{
    // HP bar Display SPRITE;
    public RectTransform BaseHP_S;
    public RectTransform Shield1_S;
    public RectTransform Shield2_S;
    public RectTransform Shield3_S;

    //TextMeshPro Display TEXT;
    public GameObject BaseHP_T;
    public GameObject Shield1_T;
    public GameObject Shield2_T;
    public GameObject Shield3_T;

    //Player Status Display VALUE;
    public int BaseHP_V;
    public int Shield1_V;
    public int Shield2_V;
    public int Shield3_V;

    private int CurrentHp;

    // Update is called once per frame

    private void Start()
    {
        CurrentHp = BaseHP_V;
    }

    void FixedUpdate()
    {
        OnLoadStatus();
   
        if (Input.GetKey(KeyCode.P))
        {
            
            takeDamage(2);
        }
    }
    void Update()
    {

     
    }
    void OnLoadStatus()
    {
        BaseHP_T.GetComponent<TMPro.TextMeshProUGUI>().text = CurrentHp +"/"+ BaseHP_V;
    }


    void takeDamage(int dmg)
    {
        float DamagePercentage = dmg / 100;
        BaseHP_S.sizeDelta = new Vector2(BaseHP_S.sizeDelta.x, BaseHP_S.sizeDelta.y-DamagePercentage);
        Debug.Log("takeDamage");


        CurrentHp -= dmg;

    }
    



}

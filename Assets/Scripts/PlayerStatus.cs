using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    
    public int maxHp;
    public int baseSpeed;
    public int poisonDamage;

    private int hp;
    private ShieldList sl;
    private Dictionary<string, int> status = new Dictionary<string, int>();


    public GameObject BaseHP_T;
    public GameObject Shield1_T;
    public GameObject Shield2_T;
    public GameObject Shield3_T;



    void Start()
    {
        hp = maxHp;
        
        sl = new ShieldList();

        status.Add("sleepy", 0);
        status.Add("poison", 0);
        status.Add("fragile", 0);

        status.Add("focus", 0);
        status.Add("strength", 0);
        status.Add("speed", 0);
    }

    void Update()
    {
        OnLoadStatusDisplay();

    }



    public int GetHp() {
        return hp;
    }

    public ShieldList GetShieldList() {
        return sl;
    }

    public int GetSpeed() {
        int r = baseSpeed;
        if (status["sleepy"] > 0) {
            r--;
        }
        if (status["speed"] > 0) {
            r++;
        }
        return r;
    }

    public int GetStatus(string stat) {
        return status[stat];
    }

    //NOTE: for positive effects, give yourself what the card says +1 bc you lose 1 turn's worth of the effect right away
    public void ApplyStatus(string stat, int amount) {
        status[stat] += amount;
    }


    public void TakeDamage(int dam) {
        hp -= dam;
        if (hp <= 0) {
            Die();
        }
    }

    public void StartTurn() {
        if (status["poison"] > 0) {
            TakeDamage(poisonDamage);
        }
    }
    
    public void EndTurn() {
        foreach(KeyValuePair<string,int> stat in status) {
            status[stat.Key] = Mathf.Max(0, stat.Value-1);
        }
    }

    private void Die() {
        Debug.Log("PlayerDed");
    }


    //Tim Addition
    void OnLoadStatusDisplay()
    {
        BaseHP_T.GetComponent<TMPro.TextMeshProUGUI>().text = hp + "/" + maxHp;
        Shield1_T.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        Shield2_T.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        Shield3_T.GetComponent<TMPro.TextMeshProUGUI>().text = "";
    }









}

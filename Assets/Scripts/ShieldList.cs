using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ShieldType { phys, ice, energy };

public class ShieldList
{
    List<Shield> s = new List<Shield>();

    public void addShield(ShieldType st, int a) {
        if (st == s[s.Count-1].st) {
            s[s.Count-1].Add(a);
        } else {
            s.Add(new Shield(st, a));
        }
    }

    public (ShieldType, int) getShield(int i) {
        return (s[i].st, s[i].amount);
    }

    public int DamageShields(int dam) {
        //Returns the damage to hp if shields are destroyed
        if (s.Count != 0) {
            if (dam > s[s.Count-1].amount) {
                int a = dam - s[s.Count-1].amount;
                s.RemoveAt(s.Count-1);
                return DamageShields(a);
            } else {
                s[s.Count-1].Damage(dam);
                return 0;
            }
        } else {
            return dam;
        }
    }

}

public class Shield
{
    public ShieldType st;
    public int amount;

    public Shield(ShieldType st_in, int a) {
        st = st_in;
        amount = a;
    }

    public void Damage(int dam) {
        amount -= dam;
    }
    
    public void Add(int a) {
        amount += a;
    }
}

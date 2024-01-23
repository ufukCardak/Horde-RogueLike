using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage
{
    int damage;
    bool isCrit;
    string attackType;

    public PlayerDamage(int damage, bool isCrit, string attackType)
    {
        this.Damage = damage;
        this.IsCrit = isCrit;
        this.attackType = attackType;
    }

    public int Damage { get => damage; set => damage = value; }
    public bool IsCrit { get => isCrit; set => isCrit = value; }
    public string AttackType { get => attackType; set => attackType = value; }
}

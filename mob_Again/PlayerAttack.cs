using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour 
{
    [SerializeField] private int attackDamage = 1;

    public int GetDamage() 
    {
        return attackDamage;
    }
}